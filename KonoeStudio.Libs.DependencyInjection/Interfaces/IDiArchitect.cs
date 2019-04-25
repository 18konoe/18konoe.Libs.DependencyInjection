namespace KonoeStudio.Libs.DependencyInjection.Interfaces
{
    public interface IDiArchitect
    {
        IDiBlueprint CreateBlueprint<TClass>(bool isSingleton = true, bool isLazyInitialize = true, params IDiArgumentInfo[] argumentList);
    }
}