using System;
using System.Collections.Generic;
using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiBlueprintTests
    {
        [TestMethod]
        public void DiBlueprintTest_OnlyType()
        {
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor));
            blueprint.ClassType.Is(typeof(NoConstructor));
        }

        [TestMethod]
        public void DiBlueprintTest_Throw_Exception()
        {
            AssertEx.Throws<InvalidOperationException>(() => new DiBlueprint(typeof(INoConstructor)));
        }

        [TestMethod]
        public void DiBlueprintTest_With_ArgumentList()
        {
            List<IDiArgumentInfo> list = new List<IDiArgumentInfo>();
            list.Add(new DiArgumentInfo<int>(0));
            list.Add(new DiArgumentInfo<string>("test"));

            IDiBlueprint blueprint = new DiBlueprint(typeof(LiteralConstructor), list);

            blueprint.ClassType.Is(typeof(LiteralConstructor));
            blueprint.ArgumentInfoList[0].ArgumentType.Is(typeof(int));
            blueprint.ArgumentInfoList[1].ArgumentType.Is(typeof(string));
            blueprint.ArgumentInfoList[0].ArgumentValue.Is(0);
            blueprint.ArgumentInfoList[1].ArgumentValue.Is("test");
        }

        [TestMethod]
        public void DiBlueprintTest_Properties_Initialized()
        {
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor));

            blueprint.IsSingleton.IsTrue();
            blueprint.IsLazyInitialize.IsTrue();
        }

        [TestMethod]
        public void DiBlueprintTest_IsSingleton_False()
        {
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor))
            {
                IsSingleton = false
            };

            blueprint.IsSingleton.IsFalse();
            blueprint.IsLazyInitialize.IsTrue();
        }

        [TestMethod]
        public void DiBlueprintTest_IsSLazyInitialize_False()
        {
            IDiBlueprint blueprint = new DiBlueprint(typeof(NoConstructor))
            {
                IsLazyInitialize = false
            };

            blueprint.IsSingleton.IsTrue();
            blueprint.IsLazyInitialize.IsFalse();
        }

        [TestMethod]
        public void DiBlueprintTest_Change_Properties_Throw_Exceptions()
        {
            AssertEx.Throws<InvalidOperationException>(() =>
            {
                new DiBlueprint(typeof(NoConstructor))
                {
                    IsSingleton = false,
                    IsLazyInitialize = false
                };
            });
        }
    }
}