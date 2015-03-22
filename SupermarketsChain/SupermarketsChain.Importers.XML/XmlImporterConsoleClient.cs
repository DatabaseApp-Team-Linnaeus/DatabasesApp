namespace SupermarketsChain.Importers.Xml
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
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

                Console.WriteLine("File not found. Stack trace:\n" + e.StackTrace);
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

        private static void AddVendorExpense(string vendorName, DateTime expenseDate, decimal expenseAmount)
        {
            var context = ObjectFactory.Get<ISupermarketsChainData>();
            int vendorId;
            var allVendors = context.Vendors.All();
            
            if (allVendors.Any(v => v.Name == vendorName))
            {
                vendorId = allVendors.FirstOrDefault(v => v.Name == vendorName).Id;
            }
            else
            {
                var newVendor = new Vendor { Name = vendorName };

                context.Vendors.Add(newVendor);
                context.SaveChanges();
                Console.WriteLine("Added Vendor {0}", vendorName);
                vendorId = newVendor.Id;
            }

            var allExpenses = context.Expenses.All();
            var expenses = context.Expenses;

            var duplicateExpense = allExpenses
                .Any(e =>
                    e.DateOfExpense == expenseDate
                    && e.ExpenseAmount == expenseAmount
                    && e.VendorId == vendorId);

            if (!duplicateExpense)
            {
                expenses.Add(
                    new Expense { VendorId = vendorId, DateOfExpense = expenseDate, ExpenseAmount = expenseAmount });
                context.SaveChanges();
                Console.WriteLine("     Added expense to Vendor {0}",vendorName);
            }
        }
    }
}
