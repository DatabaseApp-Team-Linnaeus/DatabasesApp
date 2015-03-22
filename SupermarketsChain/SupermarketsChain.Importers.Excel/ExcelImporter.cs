namespace SupermarketsChain.Importers.ZipExcel
{
    using System;
    using System.Globalization;
    using System.Data;
    using Excel = Microsoft.Office.Interop.Excel;
    using System.IO;
    using System.IO.Compression;

    internal class ExcelImporter
    {
        private string zipPath;
        private string zipName;

        public ExcelImporter(string zipPath, string zipName)
        {
            this.ZipPath = zipPath;
            this.ZipName = zipName;
        }

        public string ZipPath { get; set; }
        public string ZipName { get; set; }

        public DataSet GetData(string pathToArchive, string archiveName)
        {
            ExtractZip(pathToArchive, archiveName);
            var dataSet = new DataSet();
            var reportsDirectories = Directory.GetDirectories(pathToArchive);
            foreach (var directory in reportsDirectories)
            {
                var directoryName = Path.GetFileName(directory);
                DateTime date = DateTime.ParseExact(directoryName, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                Console.WriteLine(date);
                var files = Directory.GetFiles(directory);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    string superMarketName = fileName.Substring(0, fileName.Length - "-Sales-Report-20-Jul-2014.xls".Length);
                    Console.WriteLine(superMarketName);

                    var application = new Excel.Application();
                    var workbook = application.Workbooks.Open(Directory.GetCurrentDirectory() + file.Substring(1, file.Length- 1));
                    var worksheet = workbook.ActiveSheet;
                    Excel.Range excelRange = worksheet.UsedRange;
                    object[,] valueArray = (object[,])excelRange.get_Value(
                        Excel.XlRangeValueDataType.xlRangeValueDefault);

                    for (int i = 4; i <= valueArray.GetLength(0)  - 1; i++)
                    {
                        Console.Write("{0}", i);
                        var productName = valueArray[i, 1];
                        var quantity = valueArray[i, 2];
                        var unitPrice = valueArray[i, 3];
                        var sum = valueArray[i, 4];
                        Console.WriteLine("{0} - {1} - {2} - {3}", productName, quantity, unitPrice, sum);
                        for (int j = 1; j <= valueArray.GetLength(1); j++)
                        {
                            //Console.Write("{0} - ", valueArray[i, j]);
                        }
                        Console.WriteLine();
                    }

                    workbook.Close();
                    application.Quit();
                    GC.Collect();
                }
            }

            return dataSet;
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
