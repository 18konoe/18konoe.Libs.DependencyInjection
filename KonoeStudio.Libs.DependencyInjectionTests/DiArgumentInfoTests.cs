using System;
using KonoeStudio.Libs.DependencyInjection;
using KonoeStudio.Libs.DependencyInjection.Interfaces;
using KonoeStudio.Libs.DependencyInjectionStub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KonoeStudio.Libs.DependencyInjectionTests
{
    [TestClass]
    public class DiArgumentInfoTests
    {
        [TestMethod]
        public void DiArgumentInfoTest()
        {
            IDiArgumentInfo argumentInfo = new DiArgumentInfo<INoMeanInterface>();
            argumentInfo.ArgumentType.Is(typeof(INoMeanInterface));
            argumentInfo.ArgumentValue.IsNull();
            argumentInfo.ForceInjection.IsFalse();

            INoMeanInterface obj = new NoMeanClass();
            argumentInfo = new DiArgumentInfo<INoMeanInterface>(obj, true);
            argumentInfo.ArgumentType.Is(typeof(INoMeanInterface));
            argumentInfo.ArgumentValue.Is(obj);
            argumentInfo.ForceInjection.IsTrue();

            AssertEx.Throws<InvalidOperationException>(() =>
                argumentInfo = new DiArgumentInfo<NoConstructor>("test"));
        }
    }
}