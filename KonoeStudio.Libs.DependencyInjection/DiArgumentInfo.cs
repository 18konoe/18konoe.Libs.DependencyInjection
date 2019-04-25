using System;
using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjection
{
    public class DiArgumentInfo<TArgumentType> : IDiArgumentInfo
    {
        public Type ArgumentType { get; }
        public object ArgumentValue { get; }
        public bool ForceInjection { get; }

        public DiArgumentInfo(object argumentValue = null, bool forceInjection = false)
        {
            if (argumentValue != null && !(argumentValue is TArgumentType))
            {
                throw new InvalidOperationException($"{nameof(argumentValue)} mast be castable to {typeof(TArgumentType).Name}");
            }

            ArgumentType = typeof(TArgumentType);
            ArgumentValue = argumentValue;
            ForceInjection = forceInjection;
        }
    }

    public class DiArgumentInfo : IDiArgumentInfo
    {
        public Type ArgumentType { get; }
        public object ArgumentValue { get; }
        public bool ForceInjection { get; }

        public DiArgumentInfo(Type type, object argumentValue = null, bool forceInjection = false)
        {
            ArgumentType = type;
            ArgumentValue = argumentValue;
            ForceInjection = forceInjection;
        }
    }
}