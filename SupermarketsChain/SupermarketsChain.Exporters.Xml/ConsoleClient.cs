// If you want to test it add this project to another solution.
// The classes tempExpense and tempVendor are not needed, they just emulate the data.
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
    using SupermarketsChain.Exporters.Xml.Dto;
    using SupermarketsChain.Models;

    public static class ConsoleClient
    {
        public static void Main(string[] args)
        {
            var vendors = new List<tempVendor>
            {
                new tempVendor("Nestle Sofia Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 30.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 40.00m),
                new tempVendor("Targovishte Bottling Company Ltd.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 200.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m),
                new tempVendor("Zagorka Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 120.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m)
            };

            var doc = CreateXDocument(vendors);

            // Modify the next two lines to save the XML in a desired location
            // Directory.CreateDirectory(@"c:\temp\XML");
            doc.Save(@"vendors.xml");

            var data = ObjectFactory.Get<ISupermarketsChainData>();

            //var obj =
            //    data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now)
            //     .GroupBy(y => new { y.Product, y.Product.Vendor })
            //        .Select(
            //            x => new TestDTO
            //            {
            //                ProductId = x.Key.Product.Id,
            //                ProductName = x.Key.Product.Name,
            //                VendorName = x.Key.Product.Vendor.Name,
            //                TotalQuantity = x.Sum(y => y.Quantity),
            //                TotalCost = x.Sum(y => y.Quantity * y.PricePerUnit),
            //            });

            var obj = from vend in data.Vendors.All()
                      join exp in data.Expenses.All() on vend.Id equals exp.VendorId
                      select new XmlDto()
                                 {
                                     ExpenseValue = exp.ExpenseAmount,
                                     VendorName = vend.Name,
                                     Date = exp.DateOfExpense,
                                 };
            
            Console.WriteLine();
        }

        // Formatted this way for clarity as to which goes where :)
        public static XDocument CreateXDocument(List<tempVendor> vendors)
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
