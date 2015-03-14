// If you want to test it add this project to another solution.
// The classes Expense and Vendor are not needed, they just emulate the data.
// TODO: Change the namespace or the project name/location in solution?
namespace SupermarketsChain.Exporters.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using XmlExporterConsoleClient;

    class ConsoleClient
    {
        static void Main(string[] args)
        {
            var vendors = new List<Vendor>
            {
                new Vendor("Nestle Sofia Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"), 30.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"),40.00m),
                 new Vendor("Targovishte Bottling Company Ltd.")
                    .WithExpenses(DateTime.Parse("Jul-2013"),200.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"),180.00m),
                new Vendor("Zagorka Corp.")
                    .WithExpenses(DateTime.Parse("Jul-2013"),120.00m)
                    .WithExpenses(DateTime.Parse("Aug-2013"),180.00m)
            };

            Console.WriteLine();
            
            // Formatted this way to be for clarity as to which goes where :)
            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("expenses-by-month",
                  from vendor in vendors
                  orderby vendor.Name
                  select new XElement("vendor",
                       new XAttribute("name", vendor.Name),
                       from e in vendor.Expenses
                       select new XElement("expenses",
                           new XAttribute("month", e.Date.ToString("MMM-yyyy", CultureInfo.InvariantCulture)), e.ExpenseValue)
                             )));

            // Modify the next two lines to save the XML in a desired location
            //Directory.CreateDirectory(@"c:\temp\XML");
            xDoc.Save(@"vendors.xml");
        }
    }
}
