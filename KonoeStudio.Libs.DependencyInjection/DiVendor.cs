using System;
using System.Data;
using System.Linq;
using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjection
{
    public sealed class DiVendor : IDiVendor, IDisposable
    {
        public IDiManufacturer Manufacturer { get; set; }
        public IDiSupplierFactory SupplierFactory { get; set; }
        public IDiArchitect BlueprintFactory { get; set; }

        public bool IsProcurable(Type type)
        {
            bool result = false;
            var suppliers = Manufacturer.Suppliers;

            if (suppliers != null)
            {
                if (suppliers.ContainsKey(type))
                {
                    result = suppliers[type].IsSuppliable;
                }

                if (result && suppliers[type].SubcontractorList.Count > 0)
                {
                    result = suppliers[type].SubcontractorList.All(pair => IsProcurable(pair.Key));
                }
            }

            return result;
        }

        public bool IsProcurable<TClass>() where TClass : class
        {
            return IsProcurable(typeof(TClass));
        }

        public bool IsRegisterable(Type type)
        {
            bool result = false;

            if (Manufacturer.Suppliers != null)
            {
                result = !Manufacturer.Suppliers.ContainsKey(type);
            }

            return result;
        }

        public bool IsRegisterable<TClass>() where TClass : class
        {
            return IsRegisterable(typeof(TClass));
        }

        public DiVendor(IDiManufacturer manufacture = null, IDiSupplierFactory supplierFactory = null, IDiArchitect blueprintFactory = null)
        {
            if (blueprintFactory == null)
            {
                blueprintFactory = new DiArchitect();
            }

            BlueprintFactory = blueprintFactory;

            if (supplierFactory == null)
            {
                supplierFactory = new DiSupplierFactory();
            }

            SupplierFactory = supplierFactory;

            if (manufacture == null)
            {
                manufacture = new DiManufacturer();
            }
            Manufacturer = manufacture;
        }

        public void Register<TClass>(IDiBlueprint blueprint) where TClass : class
        {
            Register<TClass, TClass>(blueprint);
        }

        public void Register<TInterface, TClass>(IDiBlueprint blueprint)
            where TInterface : class
            where TClass : class, TInterface
        {
            // Check TClass is not Interface
            Type classType = typeof(TClass);
            if (classType.IsInterface)
            {
                throw new InvalidOperationException("An interface cannot be registered alone.");
            }

            if (blueprint == null)
            {
                throw new NoNullAllowedException($"{nameof(Register)}<{nameof(TInterface)},{nameof(TClass)}> is not allowed argument has null.");
            }

            Manufacturer.AddSupplier(typeof(TInterface), SupplierFactory.BuildSupplier(blueprint));
        }

        public void Register<TClass>(bool isSingleton = true, bool isLazyInitialize = true) where TClass : class
        {
            Register<TClass, TClass>(isSingleton, isLazyInitialize);
        }

        public void Register<TInterface, TClass>(bool isSingleton = true, bool isLazyInitialize = true)
            where TInterface : class
            where TClass : class, TInterface
        {
            IDiBlueprint blueprint = BlueprintFactory.CreateBlueprint<TClass>(isSingleton, isLazyInitialize);
            Register<TInterface, TClass>(blueprint);
        }

        public TClass Procure<TClass>() where TClass : class
        {
            object product = Manufacturer.Order(typeof(TClass));
            if (product is TClass package)
            {
                return package;
            }
            throw new InvalidCastException($"Somehow ordered object could not cast to {typeof(TClass).Name}");
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Manufacturer.Suppliers.ForAll(pair => (pair.Value as IDisposable)?.Dispose());
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DiVendor()
        // {
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