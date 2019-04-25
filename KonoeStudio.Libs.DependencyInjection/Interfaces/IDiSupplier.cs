using System;
using System.Collections.Generic;
using System.Reflection;

namespace KonoeStudio.Libs.DependencyInjection.Interfaces
{
    public interface IDiSupplier
    {
        IReadOnlyDictionary<Type, IDiSupplier> SubcontractorList { get; }
        IReadOnlyList<Type> RequiredDependencyList { get; }
        IReadOnlyList<object> InstanceStockList { get; }
        IDiBlueprint Blueprint { get; }
        bool IsSuppliable { get; }
        ConstructorInfo Constructor { get; }
        Type ProductionType { get; }
        
        object Supply();
        void AddSubContractor(Type type, IDiSupplier subcontractor);

        bool NeedToSubcontract(Type type);
    }
}