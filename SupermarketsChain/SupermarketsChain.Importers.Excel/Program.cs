namespace SupermarketsChain.Importers.ZipExcel
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string zipPath = @".\reports";
            string zipName = "Sample-Sales-Reports.zip";
            ZipFile.ExtractToDirectory(zipName, zipPath);

            var reportsDirectories = Directory.GetDirectories(zipPath);
            foreach (var directory in reportsDirectories)
            {
                var reports = Directory.GetFiles(directory);
                foreach (var report in reports)
                {
                    var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + report + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    using (var conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                        var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

                            var adapter = new OleDbDataAdapter(cmd);
                            var ds = new DataSet();
                            adapter.Fill(ds);
                            Console.WriteLine(ds.Tables);
                        }
                        conn.Close();
                    }
                }
            }
        }
    }
}
