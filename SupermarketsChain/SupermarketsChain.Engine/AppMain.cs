namespace SupermarketsChain.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SupermarketsChain.Data;
    using SupermarketsChain.Data.Contexts;
    using SupermarketsChain.Infrastructure.Infrastructure;
    using SupermarketsChain.Models;
    using SupermarketsChain.ReportToJson;

    public class AppMain
    {
        private static SqliteDbContext SqliteDbContext = new SqliteDbContext();

        public static void Main()
        {
            while (true)
            {
                var command = Console.ReadLine();
                ProseedCommand(command);
            }
        }

        private static void ProseedCommand(string command)
        {
            var parameters = command.Split();
            DateTime startDate = DateTime.Parse(parameters[1]);
            DateTime endDate = DateTime.Parse(parameters[2]);
            switch (parameters[0])
            {
                case "export-xml":
                    // add xml export method (parameters[1], parameters[2])
                    break;
                case "export-json":
                    JsonExport.Export(startDate, endDate);
                    // add json export method (parameters[1], parameters[2])
                    break;
                case "export-pdf":
                    // add pdf export method (parameters[1], parameters[2])
                    break;
                case "migrate-data-to-sqlite":
                    MigrateDataToSqlite();
                    break;
            }
        }

        private static void MigrateDataToSqlite()
        {
            IList<Sale> sales;
            using (var data = ObjectFactory.Get<ISupermarketsChainData>())
            {
                sales = data.Sales.All().ToList();
            }

            foreach (var sale in sales)
            {
                SqliteDbContext.Sales.Add(sale);
                SqliteDbContext.SaveChanges();
            }            
        }
    }
}
