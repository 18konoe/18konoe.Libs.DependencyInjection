namespace _18konoe.Libs.DependencyInjectionStub
{
    public interface IDependedConstructor
    {
        INoConstructor NoConstructor { get; }
        ILiteralConstructor LiteralConstructor { get; }
    }
}