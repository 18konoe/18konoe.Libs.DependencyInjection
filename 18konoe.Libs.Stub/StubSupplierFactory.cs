using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjectionStub
{
    public class StubSupplierFactory : IDiSupplierFactory
    {
        public IDiSupplier BuildSupplier(IDiBlueprint blueprint)
        {
            return new StubSupplier(blueprint);
        }
    }
}