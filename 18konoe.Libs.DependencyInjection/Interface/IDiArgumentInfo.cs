using System;

namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiArgumentInfo
    {
        Type ArgumentType { get; }
        object ArgumentValue { get; }
        bool ForceInjection { get; }
    }
}