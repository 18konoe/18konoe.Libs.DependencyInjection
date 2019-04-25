using System;
using System.Data;
using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiVendorTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();

            DiVendor vendor1 = new DiVendor();
            vendor1.IsNotNull();

            DiVendor vendor2 = new DiVendor(fakeManufacture);
            vendor2.IsNotNull();

            DiVendor vendor3 = new DiVendor(fakeManufacture, fakeSupplierFactory);
            vendor3.IsNotNull();
        }

        [TestMethod]
        public void IsProcurableTest()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);

            vendor.IsProcurable(typeof(INoConstructor)).IsFalse();
            vendor.IsProcurable<INoConstructor>().IsFalse();

            vendor.Register<INoConstructor, NoConstructor>();

            vendor.IsProcurable(typeof(INoConstructor)).IsTrue();
            vendor.IsProcurable<INoConstructor>().IsTrue();
        }

        [TestMethod]
        public void IsRegisterableTest()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);

            vendor.IsRegisterable(typeof(INoConstructor)).IsTrue();
            vendor.IsRegisterable<INoConstructor>().IsTrue();

            vendor.Register<INoConstructor, NoConstructor>();

            vendor.IsRegisterable(typeof(INoConstructor)).IsFalse();
            vendor.IsRegisterable<INoConstructor>().IsFalse();
        }

        [TestMethod]
        public void RegisterTest_Throw_InvalidOperationException()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);
            vendor.IsNotNull();
            AssertEx.Throws<InvalidOperationException>(() => vendor.Register<INoConstructor>());
            AssertEx.Throws<InvalidOperationException>(() => vendor.Register<INoConstructor, INoConstructor>());

            IDiBlueprint blueprint = new DiBlueprint(typeof(LiteralConstructor));
            AssertEx.Throws<InvalidOperationException>(() => vendor.Register<ILiteralConstructor>(blueprint));
            AssertEx.Throws<InvalidOperationException>(() => vendor.Register<ILiteralConstructor, ILiteralConstructor>(blueprint));
        }

        [TestMethod]
        public void RegisterTest_Throw_NoNullAllowedException()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            IDiArchitect architect = new DiArchitect();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory, architect);
            vendor.IsNotNull();
            AssertEx.Throws<NoNullAllowedException>(() => vendor.Register<NoConstructor>(null));
            AssertEx.Throws<NoNullAllowedException>(() => vendor.Register<INoConstructor, NoConstructor>(null));
        }

        [TestMethod]
        public void RegisterTest_NoInterface_Register()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);

            vendor.Register<NoConstructor>();
            vendor.Manufacturer.Suppliers.TryGetValue(typeof(NoConstructor), out IDiSupplier sup).IsTrue();
            sup.IsNotNull();
            sup.IsInstanceOf<StubSupplier>();

            IDiBlueprint blueprint = new DiBlueprint(typeof(LiteralConstructor));
            vendor.Register<LiteralConstructor>(blueprint);
            vendor.Manufacturer.Suppliers.TryGetValue(typeof(LiteralConstructor), out IDiSupplier sup2).IsTrue();
            sup2.IsNotNull();
            sup2.IsInstanceOf<StubSupplier>();
        }

        [TestMethod]
        public void RegisterTest_Interface_Register()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);
            AssertEx.Throws<InvalidOperationException>(() => vendor.Register<INoConstructor>());

            vendor.Register<INoConstructor, NoConstructor>();
            vendor.Manufacturer.Suppliers.TryGetValue(typeof(INoConstructor), out IDiSupplier sup).IsTrue();
            sup.IsNotNull();
        }

        [TestMethod]
        public void ProcureTest_Cast_Expected()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);

            INoConstructor noConstructor = vendor.Procure<INoConstructor>();
            noConstructor.IsNotNull();
        }

        [TestMethod]
        public void ProcureTest_Throw_Exception()
        {
            StubSupplierFactory fakeSupplierFactory = new StubSupplierFactory();
            StubManufacture fakeManufacture = new StubManufacture();
            DiVendor vendor = new DiVendor(fakeManufacture, fakeSupplierFactory);

            AssertEx.Throws<InvalidCastException>(() => vendor.Procure<ILiteralConstructor>());
        }
    }
}