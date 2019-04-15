using Microsoft.VisualStudio.TestTools.UnitTesting;
using _18konoe.Libs.DependencyInjection;
using _18konoe.Libs.DependencyInjection.Interface;
using _18konoe.Libs.DependencyInjectionStub;

namespace _18konoe.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiSupplierFactoryTests
    {
        [TestMethod]
        public void BuildSupplierTest()
        {
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor));
            IDiSupplierFactory factory = new DiSupplierFactory();
            IDiSupplier supplier = factory.BuildSupplier(blueprint);

            (supplier is DiSupplier).IsTrue();
            supplier.Blueprint.IsSameReferenceAs(blueprint);
        }
    }
}