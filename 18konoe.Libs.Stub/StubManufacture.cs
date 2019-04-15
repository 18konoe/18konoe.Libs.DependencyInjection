using System;
using System.Collections.Generic;
using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjectionStub
{
    public class StubManufacture : IDiManufacturer
    {
        private Dictionary<Type, IDiSupplier> _suppliers = new Dictionary<Type, IDiSupplier>();
        private Dictionary<Type, IDiSupplier> _unResolvedSuppliers = new Dictionary<Type, IDiSupplier>();
        public IReadOnlyDictionary<Type, IDiSupplier> Suppliers => _suppliers;

        public IReadOnlyDictionary<Type, IDiSupplier> UnResolvedSuppliers => _unResolvedSuppliers;

        public void AddSupplier(Type key, IDiSupplier value)
        {
            _suppliers.Add(key, value);
        }

        public object Order(Type type)
        {
            return new NoConstructor();
        }
    }
}