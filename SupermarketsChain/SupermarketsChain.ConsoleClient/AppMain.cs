namespace SupermarketsChain.ConsoleClient
{
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class AppMain
    {
        public static void Main(string[] args)
        {
            var context = new SupermarketsChainDbContext();
            var data = new SupermarketsChainData(context);
            data.Towns.Add(new Town() { Name = "Sofia" });

            data.SaveChanges();
        }
    }
}
