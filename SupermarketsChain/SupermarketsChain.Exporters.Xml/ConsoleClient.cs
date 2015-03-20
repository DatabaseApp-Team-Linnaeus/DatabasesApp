// If you want to test it add this project to another solution.
// The classes tempExpense and tempVendor are not needed, they just emulate the data.
// TODO: Change the namespace or the project name/location in solution?
// TODO: refactor into a method so it can be run easily from other places

namespace SupermarketsChain.Exporters.Xml
{
    using System;
    using System.CodeDom;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;
    using System.Xml.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Data.Repositories;
    using SupermarketsChain.Exporters.Xml.Dto;
    using SupermarketsChain.Models;

    //using XmlExporterConsoleClient;

    public static class ConsoleClient
    {
        public static void Main(string[] args)
        {
            #region
            //var vendors = new List<tempVendor>
            //{
            //    new tempVendor("Nestle Sofia Corp.")
            //        .WithExpenses(DateTime.Parse("Jul-2013"), 30.00m)
            //        .WithExpenses(DateTime.Parse("Aug-2013"), 40.00m),
            //    new tempVendor("Targovishte Bottling Company Ltd.")
            //        .WithExpenses(DateTime.Parse("Jul-2013"), 200.00m)
            //        .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m),
            //    new tempVendor("Zagorka Corp.")
            //        .WithExpenses(DateTime.Parse("Jul-2013"), 120.00m)
            //        .WithExpenses(DateTime.Parse("Aug-2013"), 180.00m)
            //};

            //var doc = CreateXDocument(vendors);

            // Modify the next two lines to save the XML in a desired location
            // Directory.CreateDirectory(@"c:\temp\XML");
            //doc.Save(@"vendors.xml");
            #endregion
            var context = ObjectFactory.Get<ISupermarketsChainData>();
            
            var startDate = DateTime.Parse("1950-01-01");
            var endDate = DateTime.Now;
            var data = context.Sales.GetAllByDateInterval(startDate, endDate);
            
            var query = from s in context.Sales.GetAllByDateInterval(startDate, endDate)
            join v in context.Vendors.All() on s.Id equals v.Id
            orderby
              v.Name
            select new {
              v.Name,
              Vendor = from ven in context.Vendors.All() select new {
                        s.SoldDate,
                        totalSum = (s.Quantity * s.PricePerUnit)
                       }
              
            };

            var vendors = context.Vendors.All()
                .Join(context.Sales.All(),
                v => v.Id,
                s => s.Id,
                (v, s) => new { v, s })
                .OrderBy(n => n.v.Name)
                //.GroupBy(n => n.v.Name)
                .Select(x =>
                new
                    {
                        x.v.Name,
                        Sales = new
                                    {
                                        x.s.PricePerUnit,
                                        x.s.SoldDate,
                                        x.s.Quantity
                                    }
                    }).GroupBy(y=>y.Name).ToList();
            var doc = new XElement("sales");
            Console.WriteLine();
            //foreach (var q in query)
            //{
            //    var vendorXml = new XElement("sale", new XAttribute("vendor", q.Name));










            //    ////Console.WriteLine("Vendor Name: {0} \n  Date sold: {1}\n     Sum: {2}",
            //    ////    q.Name,
            //    ////    q.SoldDate.ToString("MMMM-yyy", CultureInfo.InvariantCulture),
            //    ////    q.totalSum);
            //    ////Console.WriteLine("-----------------");
            //}
        


        
         
        }

        // Formatted this way for clarity as to which goes where :)
        //private static XDocument CreateXDocument(List<tempVendor> vendors)
        //{
        //    var doc = new XDocument(
        //        new XDeclaration("1.0", "utf-8", null),
        //        new XElement(
        //            "expenses-by-month",
        //            from vendor in vendors
        //            orderby vendor.Name
        //            select new XElement(
        //                "vendor",
        //                new XAttribute("name", vendor.Name),
        //                from e in vendor.Expenses
        //                select
        //                    new XElement(
        //                    "expenses",
        //                    new XAttribute("month", e.Date.ToString("MMM-yyyy", CultureInfo.InvariantCulture)),
        //                    e.ExpenseValue))));
        //    return doc;
        //}
    }
}
