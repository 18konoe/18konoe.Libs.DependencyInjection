namespace KonoeStudio.Libs.DependencyInjectionStub
{
    public interface IComplexConstructor
    {
        INoConstructor NoConstructor { get; }
        ILiteralConstructor LiteralConstructor { get; }
        IDependedConstructor DependedConstructor { get; }
        int Arg1 { get; }
    }
}