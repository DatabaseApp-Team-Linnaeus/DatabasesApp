namespace SupermarketsChain.Exportes.PDF
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    internal class ExporterPDF
    {
        private static void Main()
        {

            var data = ObjectFactory.Get<ISupermarketsChainData>();

            var obj = data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now);

            foreach (var sales in obj)
            {
                Console.WriteLine(sales.Id);
            }

           // ExportDataToPDFTable();  
        }

        private static void ExportDataToPDFTable()
        {
            Document doc = new Document(PageSize.A4, 10, 10, 42, 35);
            try
            {
                //Create Document class object and set its size to letter and give space left, right, Top, Bottom Margin

                FileStream fs = new FileStream(
                    "Chapter1_Example1.pdf",
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);
                PdfWriter wri = PdfWriter.GetInstance(doc, fs);

                doc.Open(); //Open Document to write

                Font font8 = FontFactory.GetFont("ARIAL", 10);
                Font headerFont = FontFactory.GetFont("ARIAL", 10);
                headerFont.SetStyle(33);

                // MY TESTING 
                //font8.Color = new BaseColor(Color.Red);


                //Write some content
                Paragraph paragraph =
                    new Paragraph("Aggregated Sales Report");

                // DataTable dt = GetDataTable();

                if (true) //dt != null)
                {
                    //Craete instance of the pdf table and set the number of column in that table
                    PdfPTable PdfTable = new PdfPTable(5); //dt.Columns.Count);
                    PdfPCell PdfPCell = null;


                    //Add Header of the pdf table
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Product", headerFont)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Quantity", headerFont)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Unit Price", headerFont)));
                    PdfTable.AddCell(PdfPCell);


                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Location", headerFont)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Sum", headerFont)));
                    PdfTable.AddCell(PdfPCell);



                    //How add the data from datatable to pdf table
                    for (int rows = 0; rows < 2 /*dt.Rows.Count*/; rows++)
                    {
                        for (int column = 0; column < 5 /*dt.Columns.Count*/; column++)
                        {
                            PdfPCell =
                                new PdfPCell(
                                    new Phrase(
                                        new Chunk( /*dt.Rows[rows][column].ToString()*/ rows + " " + column, font8)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                    PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                    doc.Add(paragraph); // add paragraph to the document
                    doc.Add(PdfTable); // add pdf table to the document

                }

            }
            catch (DocumentException docEx)
            {
                //handle pdf document exception if any
            }
            catch (IOException ioEx)
            {
                // handle IO exception
            }
            catch (Exception ex)
            {
                // ahndle other exception if occurs
            }
            finally
            {
                //Close document and writer
                doc.Close();

            }
        }
    }
}
