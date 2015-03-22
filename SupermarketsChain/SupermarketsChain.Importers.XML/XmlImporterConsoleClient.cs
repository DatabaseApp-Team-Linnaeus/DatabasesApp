namespace SupermarketsChain.Importers.Xml
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;
    using SupermarketsChain.Models;

    class XmlImporterConsoleClient
    {
        private const string XmlDocumentPath = @"../../../Import/Sample-Vendor-Expenses.xml";
        static void Main(string[] args)
        {

            var doc = new XDocument();

            try
            {
                doc = XDocument.Load(XmlDocumentPath);
                Console.WriteLine("XML file has been read");

            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine("File not found. Stack trace:" + e.StackTrace);
            }

            var expensesByVendor =
                (from vendor in doc.Descendants("vendor")
                 select new
                 {
                     Name = vendor.Attribute("name").Value,
                     Expenses = vendor
                         .Elements("expenses")
                         .Select(e =>
                             new
                             {
                                 Month = DateTime.Parse(e.Attribute("month").Value),
                                 Sum = decimal.Parse(e.Value, CultureInfo.InvariantCulture)
                             })
                         .ToList()
                 })
                    .ToList();

            foreach (var vendor in expensesByVendor)
            {
                foreach (var expense in vendor.Expenses)
                {
                    AddVendorExpense(vendor.Name, expense.Month, expense.Sum);
                }
            }
        }

        private static void AddVendorExpense(string vendorName, DateTime expenseDate, decimal expenseSum)
        {
            var context = ObjectFactory.Get<ISupermarketsChainData>();            
                int vendorId;
            var dataInDb= context.Vendors.All();
                if (dataInDb.Any(v => v.Name == vendorName))
                {
                    vendorId = dataInDb.FirstOrDefault(v => v.Name == vendorName).Id;
                }
                else
                {
                    var newVendor = new Vendor { Name = vendorName };

                    context.Vendors.Add(newVendor);
                    context.SaveChanges();

                    vendorId = newVendor.Id;
                }

                context.Expenses.Add(new Expense { VendorId = vendorId, DateOfExpense = expenseDate, ExpenseAmount = expenseSum });
                context.SaveChanges();
        }
    }
}
