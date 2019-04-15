using System;
using System.Collections.Generic;
using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjection
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
        public void AddDependedArgumentInfo(Type type)
        {
            _argumentInfoList.Add(new DiArgumentInfo(type));
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