using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
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