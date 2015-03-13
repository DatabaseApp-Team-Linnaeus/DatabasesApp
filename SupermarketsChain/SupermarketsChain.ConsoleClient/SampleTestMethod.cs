namespace SupermarketsChain.ConsoleClient
{
    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class SampleTestMethod
    {
        private ISupermarketsChainData data;

        public SampleTestMethod()
        {
            this.data = ObjectFactory.Get<ISupermarketsChainData>();
        }

        public void CreateEntities()
        {
            var measure = new Measure() { Name = "kilogram" };
            this.data.Measures.Add(measure);
        }
}
}
