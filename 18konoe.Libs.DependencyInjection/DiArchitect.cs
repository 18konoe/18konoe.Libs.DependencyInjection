using _18konoe.Libs.DependencyInjection.Interface;

namespace _18konoe.Libs.DependencyInjection
{
    public class DiArchitect : IDiArchitect
    {
        public IDiBlueprint CreateBlueprint<TClass>(bool isSingleton = true, bool isLazyInitialize = true, params IDiArgumentInfo[] argumentList)
        {
            return new DiBlueprint(typeof(TClass), argumentList)
            {
                IsSingleton = isSingleton,
                IsLazyInitialize = isLazyInitialize
            };
        }
    }
}