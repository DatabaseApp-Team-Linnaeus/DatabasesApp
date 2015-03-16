// If you want to test it add this project to another solution.
// The classes Expense and Vendor are not needed, they just emulate the data.
// TODO: Change the namespace or the project name/location in solution?
// TODO: refactor into a method so it can be run easily from other places

namespace SupermarketsChain.Exporters.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    using XmlExporterConsoleClient;

    public static class ConsoleClient
    {
        public static void Main(string[] args)
        {
            var vendors = new List<Vendor>
            {
                new Vendor("Nestle Sofia Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 30.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 40.00m),
                new Vendor("Targovishte Bottling Company Ltd.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 200.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m),
                new Vendor("Zagorka Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 120.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m)
            };

            var doc = XDocument(vendors);

            // Modify the next two lines to save the XML in a desired location
            // Directory.CreateDirectory(@"c:\temp\XML");
            doc.Save(@"vendors.xml");

            var data = ObjectFactory.Get<ISupermarketsChainData>();

            //var obj =
            //      data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now)
            //       .GroupBy(y => new { y.Product, y.Product.Vendor })
            //          .Select(
            //              x => new TestDTO
            //              {
            //                  ProductId = x.Key.Product.Id,
            //                  ProductName = x.Key.Product.Name,
            //                  VendorName = x.Key.Product.Vendor.Name,
            //                  TotalQuantity = x.Sum(y => y.Quantity),
            //                  TotalCost = x.Sum(y => y.Quantity * y.PricePerUnit),
            //              });

        }


        // Formatted this way for clarity as to which goes where :)
        private static XDocument XDocument(List<Vendor> vendors)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(
                    "expenses-by-month",
                    from vendor in vendors
                    orderby vendor.Name
                    select new XElement(
                        "vendor",
                        new XAttribute("name", vendor.Name),
                        from e in vendor.Expenses
                        select
                            new XElement(
                            "expenses",
                            new XAttribute("month", e.Date.ToString("MMM-yyyy", CultureInfo.InvariantCulture)),
                            e.ExpenseValue))));
            return doc;
        }
    }
}
