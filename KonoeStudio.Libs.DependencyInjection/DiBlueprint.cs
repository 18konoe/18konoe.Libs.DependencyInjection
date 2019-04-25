using System;
using System.Collections.Generic;
using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjection
{
    internal class DiBlueprint : IDiBlueprint
    {
        private bool _isSingleton = true;
        private bool _isLazyInitialize = true;
        private readonly List<IDiArgumentInfo> _argumentInfoList;
        public Type ClassType { get;}

        public bool IsSingleton
        {
            get => _isSingleton;
            set
            {
                if (!value)
                {
                    IsLazyInitialize = true;
                }

                _isSingleton = value;
            }
        }

        public bool IsLazyInitialize
        {
            get => _isLazyInitialize;
            set
            {
                if (!value && !_isSingleton)
                {
                    throw new InvalidOperationException($"If you need to change {nameof(IsLazyInitialize)} to false, required {nameof(IsSingleton)} is true.");
                }

                _isLazyInitialize = value;
            }
        }

        public IReadOnlyList<IDiArgumentInfo> ArgumentInfoList => _argumentInfoList.AsReadOnly();
        public IDiBlueprint AppendArgumentInfo(Type type, object value = null, bool forceInjection = false)
        {
            _argumentInfoList.Add(new DiArgumentInfo(type, value, forceInjection));
            return this;
        }
        public IDiBlueprint AppendArgumentInfo<TArgType>(object value = null, bool forceInjection = false)
        {
            return AppendArgumentInfo(typeof(TArgType), value, forceInjection);
        }

        public DiBlueprint(Type type, IEnumerable<IDiArgumentInfo> argumentList = null)
        {
            if (type.IsInterface)
            {
                throw new InvalidOperationException("Cannot contain Interface into Blueprint.");
            }

            ClassType = type;
            if (argumentList == null)
            {
                argumentList = new List<IDiArgumentInfo>();
            }
            _argumentInfoList = new List<IDiArgumentInfo>(argumentList);
        }
    }
}