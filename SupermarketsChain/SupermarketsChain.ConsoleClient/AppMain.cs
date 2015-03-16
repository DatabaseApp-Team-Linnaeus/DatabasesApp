namespace SupermarketsChain.ConsoleClient
{
    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class AppMain
    {
        public static void Main(string[] args)
        {
            var data = ObjectFactory.Get<ISupermarketsChainData>();
            data.Towns.Add(new Town() { Name = "Plovdiv" });

            data.SaveChanges();
        }
    }
}
