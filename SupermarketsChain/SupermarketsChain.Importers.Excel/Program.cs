namespace SupermarketsChain.Importers.ZipExcel
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string zipPath = @".\reports";
            string zipName = "Sample-Sales-Reports.zip";

            ExcelImporter excelImporter = new ExcelImporter(zipPath, zipName);
            var data = excelImporter.GetSales();

            foreach (var sale in data)
            {
                Console.Write(sale.Supermarket.Name + " - ");
                Console.WriteLine(sale.SoldDate);
                Console.WriteLine(sale.Product.Name + ": ");
                Console.WriteLine("Quantity: " + sale.Quantity);
                Console.WriteLine("Price: " + sale.PricePerUnit);
            }
        }
    }
}
