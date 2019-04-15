namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiSupplierFactory
    {
        IDiSupplier BuildSupplier(IDiBlueprint blueprint);
    }
}