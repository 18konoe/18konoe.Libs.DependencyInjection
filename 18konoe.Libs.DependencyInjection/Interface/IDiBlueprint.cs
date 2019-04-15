using System;
using System.Collections.Generic;

namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiBlueprint
    {
        Type ClassType { get; }
        bool IsSingleton { get; }
        bool IsLazyInitialize { get; }
        IReadOnlyList<IDiArgumentInfo> ArgumentInfoList { get; }
        void AddDependedArgumentInfo(Type type);
    }
}