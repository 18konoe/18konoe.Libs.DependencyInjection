using System;
using System.Collections.Generic;
using System.Data;
using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiSupplierTests
    {
        [TestMethod]
        public void DiSupplierTest()
        {
            var arg = new DiArgumentInfo<string>("test");
            var architect = new DiArchitect();
            var blueprint = architect.CreateBlueprint<NoMeanClass>(true, true, arg);
            AssertEx.Throws<ArgumentException>(() =>
            {
                DiSupplier supplier = new DiSupplier(blueprint);
            });

            // TODO: Other conditions
        }

        [TestMethod]
        public void ConstructTest()
        {
            var arg = new DiArgumentInfo<INoMeanInterface>(null);
            var architect = new DiArchitect();
            var blueprint = architect.CreateBlueprint<HaveNoMeanConstructor>(true, true, arg);
            var supplier = new DiSupplier(blueprint);

            var stubSup = new StubSupplier(new DiBlueprint(typeof(NoMeanClass)), true);
            stubSup.NullSupplyCondition = true;

            
            var supplierDynamic = supplier.AsDynamic();
            (supplierDynamic._subcontractorList as Dictionary<Type, IDiSupplier>).Add(typeof(INoMeanInterface),
                stubSup);

            AssertEx.Throws<NoNullAllowedException>(() => supplier.Construct());

            // TODO: Other conditions
        }

        [TestMethod]
        public void SupplyTest()
        {
            var supplier1 = new DiSupplier(new DiBlueprint(typeof(ComplexConstructor)));
            AssertEx.Throws<InvalidOperationException>(() => supplier1.Supply());

            var supplier2 = new DiSupplier(new DiBlueprint(typeof(NoMeanClass)));
            supplier2.InstanceStockList.Count.Is(0);

            supplier2.Supply();
            supplier2.InstanceStockList.Count.Is(1);

            supplier2.Supply();
            supplier2.InstanceStockList.Count.Is(1);

            var blueprint = new DiBlueprint(typeof(NoMeanClass))
            {
                IsSingleton = false
            };
            var supplier3 = new DiSupplier(blueprint);
            supplier3.InstanceStockList.Count.Is(0);

            supplier3.Supply();
            supplier3.InstanceStockList.Count.Is(1);

            supplier3.Supply();
            supplier3.InstanceStockList.Count.Is(2);
        }

        [TestMethod]
        public void AddSubContractorTest()
        {
            var supplier = new DiSupplier(new DiBlueprint(typeof(NoMeanClass)));
            AssertEx.Throws<InvalidOperationException>(() =>
                supplier.AddSubContractor(typeof(NoMeanClass), new StubSupplier(new DiBlueprint(typeof(NoMeanClass)))));

            var supplierDynamic = supplier.AsDynamic();
            (supplierDynamic._requiredDependencyList as List<Type>).Add(typeof(NoMeanClass));
            (supplierDynamic._subcontractorList as Dictionary<Type, IDiSupplier>).Add(typeof(NoMeanClass), new StubSupplier(new DiBlueprint(typeof(NoMeanClass))));
            AssertEx.Throws<InvalidOperationException>(() =>
                supplier.AddSubContractor(typeof(NoMeanClass), new StubSupplier(new DiBlueprint(typeof(NoMeanClass)))));

            // TODO: Other conditions
        }

        [TestMethod]
        public void NeedToSubcontractTest()
        {
            var supplier = new DiSupplier(new DiBlueprint(typeof(NoMeanClass)));
            AssertEx.Throws<InvalidOperationException>(() =>
                supplier.AddSubContractor(typeof(NoMeanClass), new StubSupplier(new DiBlueprint(typeof(NoMeanClass)))));

            var supplierDynamic = supplier.AsDynamic();
            (supplierDynamic._requiredDependencyList as List<Type>).Add(typeof(NoMeanClass));

            supplier.NeedToSubcontract(typeof(NoConstructor)).IsFalse();
            supplier.NeedToSubcontract(typeof(NoMeanClass)).IsTrue();
        }

        [TestMethod]
        public void DisposeTest()
        {
            var blueprint = new DiBlueprint(typeof(DisposeClass))
            {
                IsSingleton = false
            };
            var supplier = new DiSupplier(blueprint);

            object obj1 = supplier.Supply();
            object obj2 = supplier.Supply();

            supplier.InstanceStockList.Count.Is(2);

            supplier.Dispose();
        }
    }
}