using System;
using System.Collections.Generic;

namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiManufacturer
    {

        IReadOnlyDictionary<Type, IDiSupplier> Suppliers { get; }
        IReadOnlyDictionary<Type, IDiSupplier> UnResolvedSuppliers { get; }
        void AddSupplier(Type key, IDiSupplier value);
        object Order(Type type);
    }
}