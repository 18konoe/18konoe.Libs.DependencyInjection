namespace _18konoe.Libs.DependencyInjection.Interface
{
    public interface IDiArchitect
    {
        IDiBlueprint CreateBlueprint<TClass>(bool isSingleton = true, bool isLazyInitialize = true, params IDiArgumentInfo[] argumentList);
    }
}