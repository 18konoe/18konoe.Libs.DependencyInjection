using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjectionStub
{
    public class StubSupplierFactory : IDiSupplierFactory
    {
        public IDiSupplier BuildSupplier(IDiBlueprint blueprint)
        {
            return new StubSupplier(blueprint);
        }
    }
}