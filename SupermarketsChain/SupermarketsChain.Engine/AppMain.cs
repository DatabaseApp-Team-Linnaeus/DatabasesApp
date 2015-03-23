namespace SupermarketsChain.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Data.Contexts;
    using SupermarketsChain.Models;

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
            switch (parameters[0])
            {
                case "export-xml":
                    // add xml export method (parameters[1], parameters[2])
                    break;
                case "export-json":
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
            IList<Measure> measures;
            IList<Town> towns;
            IList<ProductTax> productTaxes;
            IList<Expense> expences;
            IList<Vendor> vendors;
            IList<Supermarket> supermarkets;
            IList<Product> products;
            IList<Sale> sales;
            using (var data = ObjectFactory.Get<ISupermarketsChainData>())
            {
                measures = data.Measures.All().ToList();
                towns = data.Towns.All().ToList();
                productTaxes = data.ProductTaxes.All().ToList();
                expences = data.Expenses.All().ToList();
                vendors = data.Vendors.All().ToList();
                supermarkets = data.Supermarkets.All().ToList();
                products = data.Products.All().ToList();
                sales = data.Sales.All().ToList();
            }

            foreach (var sale in sales)
            {
                SqliteDbContext.Sales.Add(sale);
                SqliteDbContext.SaveChanges();
            }

            //foreach (var product in products)
            //{
            //    SqliteDbContext.Products.Add(product);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var measure in measures)
            //{
            //    SqliteDbContext.Measures.Add(measure);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var town in towns)
            //{
            //    SqliteDbContext.Towns.Add(town);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var productTax in productTaxes)
            //{
            //    SqliteDbContext.ProductTaxes.Add(productTax);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var expence in expences)
            //{
            //    SqliteDbContext.Expenses.Add(expence);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var vendor in vendors)
            //{
            //    SqliteDbContext.Vendors.Add(vendor);
            //    SqliteDbContext.SaveChanges();
            //}

            //foreach (var supermarket in supermarkets)
            //{
            //    SqliteDbContext.Supermarkets.Add(supermarket);
            //    SqliteDbContext.SaveChanges();
            //}

            
        }
    }
}
