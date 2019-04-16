using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjection
{
    internal class DiSupplier : IDiSupplier
    {
        private readonly Dictionary<Type, IDiSupplier> _subcontractorList = new Dictionary<Type, IDiSupplier>();
        private readonly List<Type> _requiredDependencyList = new List<Type>();
        private readonly List<object> _instanceStockList = new List<object>();

        public IReadOnlyDictionary<Type, IDiSupplier> SubcontractorList => new ReadOnlyDictionary<Type, IDiSupplier>(_subcontractorList);
        public IReadOnlyList<Type> RequiredDependencyList => _requiredDependencyList.AsReadOnly();
        public IReadOnlyList<object> InstanceStockList => _instanceStockList.AsReadOnly();

        public bool IsSuppliable { get; private set; }
        public IDiBlueprint Blueprint { get; }
        public ConstructorInfo Constructor { get; private set; }

        public Type ProductionType => Blueprint.ClassType;

        public DiSupplier(IDiBlueprint blueprint)
        {
            Blueprint = blueprint;
            ConstructorInfo[] constructorInfos = ProductionType.GetConstructors();

            if (blueprint.ArgumentInfoList.Count == 0)
            {
                Constructor = ProductionType.GetConstructor(Type.EmptyTypes);

                if (Constructor == null)
                {
                    Constructor = constructorInfos[0];
                    ParameterInfo[] parameters = Constructor.GetParameters();
                    parameters.ForAll(parameter =>
                    {
                        _requiredDependencyList.Add(parameter.ParameterType);
                        Blueprint.AddDependedArgumentInfo(parameter.ParameterType);
                    });
                }
            }
            else
            {
                Type[] argumentTypes = Blueprint.ArgumentInfoList.Select(info => info.ArgumentType).ToArray();
                
                Constructor = ProductionType.GetConstructor(argumentTypes);

                if (Constructor == null)
                {
                    throw new ArgumentException($"{ProductionType.Name} has no matched constructor with this {nameof(Blueprint)}");
                }

                Blueprint.ArgumentInfoList.Where(info => info.ArgumentValue == null && !info.ForceInjection).ForAll(info => _requiredDependencyList.Add(info.ArgumentType));
            }

            if (_requiredDependencyList.Count == 0)
            {
                IsSuppliable = true;
            }

            // If info has IsLazyInitialize = false, create instance immediately.
            if (IsSuppliable && !blueprint.IsLazyInitialize)
            {
                _instanceStockList.Add(Construct());
            }
        }
        public object Construct()
        {
            object[] args = Blueprint.ArgumentInfoList.Select(info =>
            {
                if (info.ArgumentValue == null && !info.ForceInjection)
                {
                    IDiSupplier subcontractor = _subcontractorList[info.ArgumentType];
                    object arg = subcontractor.Supply();
                    if (arg == null)
                    {
                        throw new NoNullAllowedException($"{nameof(subcontractor)}({info.ArgumentType}) is sappliable but somehow return null");
                    }

                    return arg;
                }

                return info.ArgumentValue;
            }).ToArray();
            return Constructor.Invoke(args);
        }
       
        public object Supply()
        {
            if (!IsSuppliable)
            {
                throw new InvalidOperationException($"This {nameof(DiSupplier)} cannot {nameof(Supply)}");
            }

            // If info has IsSingleton = true, keep only 1 instance in List.
            if (Blueprint.IsSingleton)
            {
                if (_instanceStockList.Count == 0)
                {
                    _instanceStockList.Add(Construct());
                }
            }
            else
            {
                _instanceStockList.Add(Construct());
            }

            return _instanceStockList.Last();
        }

        public void AddSubContractor(Type type, IDiSupplier subcontractor)
        {
            if (!_requiredDependencyList.Contains(type))
            {
                throw new InvalidOperationException($"This supplier do not require {nameof(subcontractor)} of {subcontractor.ProductionType.Name}");
            }

            _requiredDependencyList.Remove(type);

            if (_subcontractorList.ContainsKey(type))
            {
                throw new InvalidOperationException($"This supplier is already added in {nameof(_subcontractorList)}");
            }

            _subcontractorList.Add(type, subcontractor);

            if (_requiredDependencyList.Count == 0)
            {
                IsSuppliable = true;
            }
        }

        public bool NeedToSubcontract(Type type)
        {
            return _requiredDependencyList.Contains(type);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _instanceStockList.ForAll(obj => (obj as IDisposable)?.Dispose());
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DiSupplier() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}