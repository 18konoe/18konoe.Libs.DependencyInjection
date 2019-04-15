namespace _18konoe.Libs.DependencyInjectionStub
{
    public class ComplexConstructor : IComplexConstructor
    {
        public INoConstructor NoConstructor { get; }
        public ILiteralConstructor LiteralConstructor { get; }
        public IDependedConstructor DependedConstructor { get; }
        public int Arg1 { get; }

        public ComplexConstructor(INoConstructor noConstructor, ILiteralConstructor literalConstructor, IDependedConstructor dependedConstructor, int arg1)
        {
            NoConstructor = noConstructor;
            LiteralConstructor = literalConstructor;
            DependedConstructor = dependedConstructor;
            Arg1 = arg1;
        }
    }
}