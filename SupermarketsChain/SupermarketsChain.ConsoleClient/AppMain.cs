namespace SupermarketsChain.ConsoleClient
{
    using System;
    using System.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    public class AppMain
    {
        public static void Main(string[] args)
        {
            var data = ObjectFactory.Get<ISupermarketsChainData>();

            Console.WriteLine(data.Measures.All().FirstOrDefault());

        }
    }
}
