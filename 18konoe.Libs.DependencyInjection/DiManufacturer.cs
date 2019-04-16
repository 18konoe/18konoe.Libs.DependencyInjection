using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjection
{
    internal class DiManufacturer : IDiManufacturer
    {
        private readonly Dictionary<Type, IDiSupplier> _suppliers = new Dictionary<Type, IDiSupplier>();
        private readonly Dictionary<Type, IDiSupplier> _unResolvedSuppliers = new Dictionary<Type, IDiSupplier>();

        public IReadOnlyDictionary<Type, IDiSupplier> Suppliers
        {
            get
            {
                ReadOnlyDictionary<Type, IDiSupplier> dictionary = null;
                if (_suppliers != null)
                {
                    lock (_suppliers)
                    {
                        dictionary = new ReadOnlyDictionary<Type, IDiSupplier>(_suppliers);
                    }
                }

                return dictionary;
            }
        }

        public IReadOnlyDictionary<Type, IDiSupplier> UnResolvedSuppliers
        {
            get
            {
                ReadOnlyDictionary<Type, IDiSupplier> dictionary = null;
                if (_suppliers != null)
                {
                    lock (_unResolvedSuppliers)
                    {
                        dictionary = new ReadOnlyDictionary<Type, IDiSupplier>(_unResolvedSuppliers);
                    }
                }

                return dictionary;
            }
        }

        public void AddSupplier(Type key, IDiSupplier supplier)
        {
            IDiSupplier addUnResolvedSupplier = null;
            Dictionary<Type, IDiSupplier> temp = new Dictionary<Type, IDiSupplier>();

            lock (_suppliers)
            {
                if (_suppliers.ContainsKey(key))
                {
                    throw new InvalidOperationException($"{key.Name} is already registered.");
                }

                supplier.RequiredDependencyList.ForAll(type =>
                {
                    if (_suppliers.ContainsKey(type))
                    {
                        temp.Add(type, _suppliers[type]);
                    }
                });

                temp.ForAll(pair => supplier.AddSubContractor(pair.Key, pair.Value));

                _suppliers.Add(key, supplier);

                if (!supplier.IsSuppliable)
                {
                    addUnResolvedSupplier = supplier;
                }

            }

            if (addUnResolvedSupplier != null)
            {
                lock (_unResolvedSuppliers)
                {
                    _unResolvedSuppliers.Add(key, supplier);
                }
            }
            CheckUnResolvedSupplier(key, supplier);
        }

        public object Order(Type type)
        {
            IDiSupplier supplier;
            bool supplierExist;
            lock (_suppliers)
            {
                supplierExist = _suppliers.TryGetValue(type, out supplier);
            }

            if (!supplierExist)
            {
                throw new InvalidOperationException($"No {nameof(IDiSupplier)} has capability to supply instance of {type.Name}");
            }

            return supplier.Supply();
        }

        public void CheckUnResolvedSupplier(Type key, IDiSupplier supplier)
        {
            Dictionary<Type, IDiSupplier> temp = new Dictionary<Type, IDiSupplier>();

            lock (_unResolvedSuppliers)
            {
                _unResolvedSuppliers.ForAll(pair =>
                {
                    if (pair.Value.NeedToSubcontract(key))
                    {
                        pair.Value.AddSubContractor(key, supplier);
                        if (pair.Value.IsSuppliable)
                        {
                            temp.Add(pair.Key, pair.Value);
                        }
                    }
                });
                temp.ForAll(pair => _unResolvedSuppliers.Remove(pair.Key));
            }

            temp.ForAll(pair => CheckUnResolvedSupplier(pair.Key, pair.Value));
        }
    }
}