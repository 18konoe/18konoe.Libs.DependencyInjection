using KonoeStudio.Libs.DependencyInjection.Interfaces;

namespace KonoeStudio.Libs.DependencyInjection
{
    internal class DiSupplierFactory: IDiSupplierFactory
    {
        public IDiSupplier BuildSupplier(IDiBlueprint blueprint)
        {
            return new DiSupplier(blueprint);
        }
    }
}