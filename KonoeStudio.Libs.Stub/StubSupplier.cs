using System;
using System.Collections.Generic;
using System.Reflection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjectionStub
{
    public class StubSupplier : IDiSupplier
    {
        private readonly Dictionary<Type, IDiSupplier> _subcontractorList = new Dictionary<Type, IDiSupplier>();
        private readonly List<Type> _requiredDependencyList = new List<Type>();
        public bool NullSupplyCondition { get; set; } = false;
        public IReadOnlyDictionary<Type, IDiSupplier> SubcontractorList => _subcontractorList;

        public IReadOnlyList<Type> RequiredDependencyList => _requiredDependencyList;

        public IReadOnlyList<object> InstanceStockList => throw new NotImplementedException();

        public IDiBlueprint Blueprint { get; set; }

        public bool IsSuppliable { get; set; }

        public ConstructorInfo Constructor => throw new NotImplementedException();

        public Type ProductionType => Blueprint.ClassType;

        public void AddSubContractor(Type type, IDiSupplier subcontractor)
        {
            if (_requiredDependencyList.Contains(type))
            {
                _subcontractorList.Add(type, subcontractor);
            }

            if (_requiredDependencyList.Count == 0)
            {
                IsSuppliable = true;
            }
        }

        public bool NeedToSubcontract(Type type)
        {
            return _requiredDependencyList.Contains(type);
        }

        public object Supply()
        {
            if (NullSupplyCondition)
            {
                return null;
            }
            return new object();
        }

        public StubSupplier(IDiBlueprint blueprint, bool fakeIsSuppliable = true, Type addRequired = null)
        {
            Blueprint = blueprint;
            IsSuppliable = fakeIsSuppliable;
            if (!fakeIsSuppliable)
            {
                _requiredDependencyList.Add(addRequired);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~StubSupplier() {
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