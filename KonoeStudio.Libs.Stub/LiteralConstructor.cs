namespace KonoeStudio.Libs.DependencyInjectionStub
{
    public class LiteralConstructor : ILiteralConstructor
    {
        public LiteralConstructor(int num, string str)
        {
            Num = num;
            Str = str;
        }

        public int Num { get; }
        public string Str { get; }
    }
}