namespace SupermarketsChain.Importers.ZipExcel
{
    using System;
    using System.Data;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string zipPath = @".\reports";
            string zipName = "Sample-Sales-Reports.zip";

            ExcelImporter excelImporter = new ExcelImporter(zipPath, zipName);
            DataSet data = excelImporter.GetData(zipPath, zipName);
            
        }
    }
}
