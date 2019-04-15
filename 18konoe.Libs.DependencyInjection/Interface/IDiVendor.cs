using System;

namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiVendor
    {
        IDiManufacturer Manufacturer { get; set; }
        IDiSupplierFactory SupplierFactory { get; set; }

        IDiArchitect BlueprintFactory { get; set; }
        bool IsProcurable(Type type);
        bool IsProcurable<TClass>() where TClass : class;
        bool IsRegisterable(Type type);

        bool IsRegisterable<TClass>() where TClass : class;
        void Register<TClass>(IDiBlueprint blueprint) where TClass : class;
        void Register<TInterface, TClass>(IDiBlueprint blueprint)
            where TInterface : class
            where TClass : class, TInterface;
        void Register<TClass>(bool isSingleton = true, bool isLazyInitialize = true) where TClass : class;
        void Register<TInterface, TClass>(bool isSingleton = true, bool isLazyInitialize = true)
            where TInterface : class
            where TClass : class, TInterface;

        TClass Procure<TClass>() where TClass : class;
    }
}