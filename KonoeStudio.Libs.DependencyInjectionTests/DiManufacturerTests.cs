using System;
using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiManufacturerTests
    {
        [TestMethod]
        public void AddSupplierTest()
        {
            IDiManufacturer manufacturer = new DiManufacturer();
            IDiBlueprint blueprint = new DiBlueprint(typeof(DependedConstructor));
            IDiSupplier disabledSupplier = new StubSupplier(blueprint, false, typeof(INoConstructor));
            IDiSupplier enabledSupplier = new StubSupplier(blueprint);

            manufacturer.AddSupplier(typeof(IDependedConstructor), disabledSupplier);
            manufacturer.Suppliers.ContainsKey(typeof(IDependedConstructor)).IsTrue();
            manufacturer.UnResolvedSuppliers[typeof(IDependedConstructor)].Is(disabledSupplier);

            AssertEx.Throws<InvalidOperationException>((() =>
                manufacturer.AddSupplier(typeof(IDependedConstructor), enabledSupplier)));

        }

        [TestMethod]
        public void OrderTest()
        {
            IDiManufacturer manufacturer = new DiManufacturer();
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor));
            IDiSupplier disabledSupplier = new StubSupplier(blueprint, false, typeof(INoConstructor));
            IDiSupplier enabledSupplier = new StubSupplier(blueprint);

            AssertEx.Throws<InvalidOperationException>(() => manufacturer.Order(typeof(INoConstructor)));

            manufacturer.AddSupplier(typeof(INoConstructor), enabledSupplier);
            object result = manufacturer.Order(typeof(INoConstructor));
            result.IsNotNull();
        }


        [TestMethod]
        public void CheckUnResolvedSupplierTest()
        {
            // TODO
        }
    }
}