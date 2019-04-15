using Microsoft.VisualStudio.TestTools.UnitTesting;
using _18konoe.Libs.DependencyInjection;
using _18konoe.Libs.DependencyInjectionStub;

namespace _18konoe.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiFunctionalTest
    {
        [TestMethod]
        public void Tear1_Singleton_Lazy_NoArgs_NoInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<NoConstructor>();

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<NoConstructor>();
            var result2 = vendor.Procure<NoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_Lazy_NoArgs_WithInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<INoConstructor, NoConstructor>();

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<INoConstructor>();
            var result2 = vendor.Procure<INoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_NoLazy_NoArgs_NoInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<NoConstructor>(true, false);

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(1);

            var result1 = vendor.Procure<NoConstructor>();
            var result2 = vendor.Procure<NoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_NoLazy_NoArgs_WithInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<INoConstructor, NoConstructor>(true, false);

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(1);

            var result1 = vendor.Procure<INoConstructor>();
            var result2 = vendor.Procure<INoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_NotSingleton_Lazy_NoArgs_NoInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<NoConstructor>(false);

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<NoConstructor>();
            var result2 = vendor.Procure<NoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsNotSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(NoConstructor)].InstanceStockList.Count.Is(2);
        }

        [TestMethod]
        public void Tear1_NotSingleton_Lazy_NoArgs_WithInterface()
        {
            var vendor = new DiVendor();
            vendor.Register<INoConstructor, NoConstructor>(false);

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<INoConstructor>();
            var result2 = vendor.Procure<INoConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<NoConstructor>();

            result2.IsNotNull();
            result2.IsInstanceOf<NoConstructor>();
            result1.IsNotSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(INoConstructor)].InstanceStockList.Count.Is(2);
        }

        [TestMethod]
        public void Tear1_Singleton_Lazy_Args_NoInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(true, true, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<ComplexConstructor>();
            var result2 = vendor.Procure<ComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_Lazy_Args_WithInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(true, true, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<IComplexConstructor, ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<IComplexConstructor>();
            var result2 = vendor.Procure<IComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_NoLazy_Args_NoInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(true, false, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(1);

            var result1 = vendor.Procure<ComplexConstructor>();
            var result2 = vendor.Procure<ComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_Singleton_NoLazy_Args_WithInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(true, false, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<IComplexConstructor, ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(1);

            var result1 = vendor.Procure<IComplexConstructor>();
            var result2 = vendor.Procure<IComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();
            result1.IsSameReferenceAs(result2);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(1);
        }

        [TestMethod]
        public void Tear1_NotSingleton_Lazy_Args_NoInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(false, true, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<ComplexConstructor>();
            var result2 = vendor.Procure<ComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();
            result2.NoConstructor.IsSameReferenceAs(arg1Value);
            result2.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result2.LiteralConstructor.Num.Is(0);
            result2.LiteralConstructor.Str.Is("test");
            result2.DependedConstructor.IsNull();
            result2.Arg1.Is(10);

            vendor.Manufacturer.Suppliers[typeof(ComplexConstructor)].InstanceStockList.Count.Is(2);
        }

        [TestMethod]
        public void Tear1_NotSingleton_Lazy_Args_WithInterface()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg2Value = new LiteralConstructor(0, "test");
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(arg2Value);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null, true);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint = architect.CreateBlueprint<ComplexConstructor>(false, true, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<IComplexConstructor, ComplexConstructor>(blueprint);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(0);

            var result1 = vendor.Procure<IComplexConstructor>();
            var result2 = vendor.Procure<IComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<IComplexConstructor>();
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result1.LiteralConstructor.Num.Is(0);
            result1.LiteralConstructor.Str.Is("test");
            result1.DependedConstructor.IsNull();
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<IComplexConstructor>();
            result2.NoConstructor.IsSameReferenceAs(arg1Value);
            result2.LiteralConstructor.IsSameReferenceAs(arg2Value);
            result2.LiteralConstructor.Num.Is(0);
            result2.LiteralConstructor.Str.Is("test");
            result2.DependedConstructor.IsNull();
            result2.Arg1.Is(10);

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(2);
        }

        [TestMethod]
        public void Tear2_Complex()
        {
            var vendor = new DiVendor();
            var architect = new DiArchitect();
            var arg1Value = new NoConstructor();
            var arg1Info = new DiArgumentInfo<INoConstructor>(arg1Value);
            var arg2Info = new DiArgumentInfo<ILiteralConstructor>(null, true);
            var arg3Info = new DiArgumentInfo<IDependedConstructor>(null);
            var arg4Info = new DiArgumentInfo<int>(10);
            var blueprint1 = architect.CreateBlueprint<ComplexConstructor>(false, true, arg1Info, arg2Info, arg3Info, arg4Info);
            vendor.Register<IComplexConstructor, ComplexConstructor>(blueprint1);
            vendor.IsProcurable<IComplexConstructor>().IsFalse();
            vendor.IsProcurable<ILiteralConstructor>().IsFalse();
            vendor.IsProcurable<IDependedConstructor>().IsFalse();
            vendor.IsProcurable<INoConstructor>().IsFalse();

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(0);

            var arg5Info = new DiArgumentInfo<int>(0);
            var arg6Info = new DiArgumentInfo<string>("test");
            var blueprint2 = architect.CreateBlueprint<LiteralConstructor>(false, true, arg5Info, arg6Info);
            vendor.Register<ILiteralConstructor, LiteralConstructor>(blueprint2);
            vendor.IsProcurable<IComplexConstructor>().IsFalse();
            vendor.IsProcurable<ILiteralConstructor>().IsTrue();
            vendor.IsProcurable<IDependedConstructor>().IsFalse();
            vendor.IsProcurable<INoConstructor>().IsFalse();

            vendor.Register<IDependedConstructor, DependedConstructor>(false, true);
            vendor.IsProcurable<IComplexConstructor>().IsFalse();
            vendor.IsProcurable<ILiteralConstructor>().IsTrue();
            vendor.IsProcurable<IDependedConstructor>().IsFalse();
            vendor.IsProcurable<INoConstructor>().IsFalse();

            vendor.Register<INoConstructor, NoConstructor>();
            vendor.IsProcurable<IComplexConstructor>().IsTrue();
            vendor.IsProcurable<ILiteralConstructor>().IsTrue();
            vendor.IsProcurable<IDependedConstructor>().IsTrue();
            vendor.IsProcurable<INoConstructor>().IsTrue();

            var result1 = vendor.Procure<IComplexConstructor>();
            var result2 = vendor.Procure<IComplexConstructor>();

            result1.IsNotNull();
            result1.IsInstanceOf<ComplexConstructor>();
            result1.IsNotSameReferenceAs(result2);
            result1.NoConstructor.IsSameReferenceAs(arg1Value);
            result1.LiteralConstructor.IsNull();
            result1.DependedConstructor.IsNotNull();
            result1.DependedConstructor.IsNotSameReferenceAs(result2.DependedConstructor);
            result1.DependedConstructor.NoConstructor.IsNotNull();
            result1.DependedConstructor.NoConstructor.IsSameReferenceAs(result2.DependedConstructor.NoConstructor);
            result1.DependedConstructor.LiteralConstructor.IsNotNull();
            result1.DependedConstructor.LiteralConstructor.IsNotSameReferenceAs(result2.DependedConstructor.LiteralConstructor);
            result1.Arg1.Is(10);

            result2.IsNotNull();
            result2.IsInstanceOf<ComplexConstructor>();

            vendor.Manufacturer.Suppliers[typeof(IComplexConstructor)].InstanceStockList.Count.Is(2);
            vendor.Manufacturer.Suppliers[typeof(IDependedConstructor)].InstanceStockList.Count.Is(2);
            vendor.Manufacturer.Suppliers[typeof(ILiteralConstructor)].InstanceStockList.Count.Is(2);
        }
    }
}
