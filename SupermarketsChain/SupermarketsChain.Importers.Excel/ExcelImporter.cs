namespace SupermarketsChain.Importers.ZipExcel
{
    using System;
    using System.Globalization;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.IO.Compression;

    internal class ExcelImporter
    {
        private const string ConnectionProvider = "Provider=Microsoft.ACE.OLEDB.12.0;";

        private const string ConnectionProperties =
            "Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\"";

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
                    var dataSource = "Data Source=" + file + ";";
                    var connectionString = ConnectionProvider + dataSource + ConnectionProperties;

                    using (var conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        var sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});

                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"] + "] ";
                            var adapter = new OleDbDataAdapter(cmd);
                            adapter.Fill(dataSet);
                        }

                        conn.Close();

                        foreach (DataTable table in dataSet.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                foreach (DataColumn column in table.Columns)
                                {
                                    Console.WriteLine(row[column]);
                                }
                            }
                        }
                    }
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
