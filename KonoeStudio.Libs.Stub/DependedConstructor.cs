namespace KonoeStudio.Libs.DependencyInjectionStub
{
    public class DependedConstructor : IDependedConstructor
    {
        public INoConstructor NoConstructor { get; }
        public ILiteralConstructor LiteralConstructor { get; }

        public DependedConstructor(INoConstructor noConstructor, ILiteralConstructor literalConstructor)
        {
            NoConstructor = noConstructor;
            LiteralConstructor = literalConstructor;
        }
    }
}