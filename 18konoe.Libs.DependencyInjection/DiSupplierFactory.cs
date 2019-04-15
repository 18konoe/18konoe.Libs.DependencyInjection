using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjection
{
    internal class DiSupplierFactory: IDiSupplierFactory
    {
        public IDiSupplier BuildSupplier(IDiBlueprint blueprint)
        {
            return new DiSupplier(blueprint);
        }
    }
}