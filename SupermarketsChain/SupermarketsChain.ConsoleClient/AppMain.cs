namespace SupermarketsChain.ConsoleClient
{
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class AppMain
    {
        public static void Main(string[] args)
        {
            var context = new SupermarketsChainDbContext();
            // this throws exeption - use only context
            //var data = new SupermarketsChainData(context);
            context.Towns.Add(new Town() { Name = "Vraca" });

            context.SaveChanges();
        }
    }
}
