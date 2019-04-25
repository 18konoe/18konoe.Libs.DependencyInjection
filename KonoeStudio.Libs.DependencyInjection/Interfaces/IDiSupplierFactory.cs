namespace KonoeStudio.Libs.DependencyInjection.Interfaces
{
    public interface IDiSupplierFactory
    {
        IDiSupplier BuildSupplier(IDiBlueprint blueprint);
    }
}