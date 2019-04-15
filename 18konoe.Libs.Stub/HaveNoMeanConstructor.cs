namespace _18konoe.Libs.DependencyInjectionStub
{
    public class HaveNoMeanConstructor
    {
        public INoMeanInterface NoMean { get; set; }

        public HaveNoMeanConstructor(INoMeanInterface noMean)
        {
            NoMean = noMean;
        }
    }
}