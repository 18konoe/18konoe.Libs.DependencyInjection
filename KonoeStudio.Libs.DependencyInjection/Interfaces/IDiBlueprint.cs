using System;
using System.Collections.Generic;

namespace KonoeStudio.Libs.DependencyInjection.Interfaces
{
    public interface IDiBlueprint
    {
        Type ClassType { get; }
        bool IsSingleton { get; }
        bool IsLazyInitialize { get; }
        IReadOnlyList<IDiArgumentInfo> ArgumentInfoList { get; }
        IDiBlueprint AppendArgumentInfo(Type type, object value = null, bool forceInjection = false);
        IDiBlueprint AppendArgumentInfo<TArgType>(object value = null, bool forceInjection = false);
    }
}