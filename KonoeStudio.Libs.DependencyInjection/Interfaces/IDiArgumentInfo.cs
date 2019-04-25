using System;

namespace KonoeStudio.Libs.DependencyInjection.Interfaces
{
    public interface IDiArgumentInfo
    {
        Type ArgumentType { get; }
        object ArgumentValue { get; }
        bool ForceInjection { get; }
    }
}