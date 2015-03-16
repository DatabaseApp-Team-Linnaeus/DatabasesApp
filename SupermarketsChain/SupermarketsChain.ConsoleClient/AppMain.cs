namespace SupermarketsChain.ConsoleClient
{
    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class AppMain
    {
        public static void Main()
        {
            var data = ObjectFactory.Get<ISupermarketsChainData>();
            data.Towns.Add(new Town() { Name = "Plovdiv" });
            data.Towns.Add(new Town() { Name = "Pleven" });

            data.SaveChanges();

        }
    }
}
