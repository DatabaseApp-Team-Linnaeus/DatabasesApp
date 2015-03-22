namespace SupermarketsChain.Importers.ZipExcel
{
    using System;
    using System.Globalization;
    using System.Collections.Generic;
    using Excel = Microsoft.Office.Interop.Excel;
    using System.IO;
    using System.IO.Compression;

    using SupermarketsChain.Models;

    internal class ExcelImporter
    {
        public ExcelImporter(string zipPath, string zipName)
        {
            this.ZipPath = zipPath;
            this.ZipName = zipName;
        }

        public string ZipPath { get; set; }
        public string ZipName { get; set; }
        public IList<Sale> SalesToBeImported { get; set; }

        public IList<Sale> GetSales()
        {
            ExtractZip(this.ZipPath, this.ZipName);
            var sales = new List<Sale>();

            var reportsDirectories = Directory.GetDirectories(this.ZipPath);
            foreach (var directory in reportsDirectories)
            {
                var directoryName = Path.GetFileName(directory);
                var files = Directory.GetFiles(directory);

                foreach (var file in files)
                {      
                    foreach (var currentSale in ReadSalesFromFile(file, directoryName))
                    {
                        sales.Add(currentSale);
                    }
                }

                this.SalesToBeImported = sales;
            }

            return this.SalesToBeImported;
        }

        private List<Sale> ReadSalesFromFile(string file, string directoryName)
        {
            var fileName = Path.GetFileName(file);
            DateTime date = DateTime.ParseExact(directoryName, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            var sales = new List<Sale>();
            var sale = new Sale();

            string superMarketName = fileName.Substring(0, fileName.Length - "-Sales-Report-dd-MMM-yyyy.xls".Length);

            sale.Supermarket = new Supermarket
            {
                Name = superMarketName
            };

            sale.SoldDate = date;

            var application = new Excel.Application();
            var workbook = application.Workbooks.Open(Directory.GetCurrentDirectory() + file.Substring(1, file.Length - 1));
            var worksheet = workbook.ActiveSheet;
            Excel.Range excelRange = worksheet.UsedRange;
            object[,] valueArray = (object[,])excelRange.get_Value(
                Excel.XlRangeValueDataType.xlRangeValueDefault);

            var rows = valueArray.GetLength(0);
            for (int i = 4; i <= rows - 1; i++)
            {
                sale.Product = new Product()
                {
                    Name = valueArray[i, 1].ToString()
                };
                sale.Quantity = int.Parse(valueArray[i, 2].ToString());
                sale.PricePerUnit = decimal.Parse(valueArray[i, 3].ToString());

                sales.Add(sale);
            }

            workbook.Close();
            application.Quit();
            GC.Collect();

            return sales;
        }

        private void ExtractZip(string pathToArchive, string archiveName)
        {
            if (Directory.Exists(pathToArchive))
            {
                Directory.Delete(pathToArchive + "\\", true);
            }

            ZipFile.ExtractToDirectory(archiveName, pathToArchive);
        }
    }
}
