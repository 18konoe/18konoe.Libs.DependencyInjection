using Microsoft.VisualStudio.TestTools.UnitTesting;
using _18konoe.Libs.DependencyInjection;
using _18konoe.Libs.DependencyInjection.Interface;
using _18konoe.Libs.DependencyInjectionStub;

namespace _18konoe.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiArchitectTests
    {
        [TestMethod]
        public void CreateBlueprintTest_NoArgs()
        {
            IDiArchitect architect = new DiArchitect();
            IDiBlueprint blueprint1 = architect.CreateBlueprint<NoConstructor>();

            blueprint1.IsNotNull();
            blueprint1.IsInstanceOf<DiBlueprint>();
            blueprint1.ClassType.Is(typeof(NoConstructor));
            blueprint1.IsSingleton.IsTrue();
            blueprint1.IsLazyInitialize.IsTrue();
        }

        [TestMethod]
        public void CreateBlueprintTest_Args1()
        {
            IDiArchitect architect = new DiArchitect();
            IDiBlueprint blueprint1 = architect.CreateBlueprint<NoConstructor>(false);

            blueprint1.IsNotNull();
            blueprint1.IsInstanceOf<DiBlueprint>();
            blueprint1.ClassType.Is(typeof(NoConstructor));
            blueprint1.IsSingleton.IsFalse();
            blueprint1.IsLazyInitialize.IsTrue();
        }

        [TestMethod]
        public void CreateBlueprintTest_Args2()
        {
            IDiArchitect architect = new DiArchitect();
            IDiBlueprint blueprint1 = architect.CreateBlueprint<NoConstructor>(true, false);

            blueprint1.IsNotNull();
            blueprint1.IsInstanceOf<DiBlueprint>();
            blueprint1.ClassType.Is(typeof(NoConstructor));
            blueprint1.IsSingleton.IsTrue();
            blueprint1.IsLazyInitialize.IsFalse();
        }
    }
}