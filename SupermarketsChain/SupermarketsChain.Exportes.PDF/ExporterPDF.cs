namespace SupermarketsChain.Exportes.PDF
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    internal class ExporterPDF
    {
        private static void Main()
        {
            // Performance
            //var data = ObjectFactory.Get<ISupermarketsChainData>();
            //var obj =
            //    data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now)
            //        .Select(
            //            s =>
            //                {
            //                    s.PricePerUnit,
            //                    s.Quantity
            //                })
            //        .ToList();

            var data = ObjectFactory.Get<ISupermarketsChainData>();
            var obj = data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now);


            var dateFromExport = new List<String>();

            foreach (var sales in obj)
            {
                dateFromExport.Add(sales.ProductId.ToString());
                dateFromExport.Add(sales.Quantity.ToString());
                dateFromExport.Add(sales.PricePerUnit.ToString());
                dateFromExport.Add(sales.SupermarketId.ToString());
                dateFromExport.Add(sales.SaleCost.ToString());

            }

            ExportDataToPDFTable(dateFromExport);
        }

        private static void ExportDataToPDFTable(List<string> dateFromExport)
        {
            var doc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                // Create Document class object and set its size to letter and give space left, right, Top, Bottom Margin
                var fs = new FileStream(
                    "Chapter1_Example1.pdf",
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);
                PdfWriter wri = PdfWriter.GetInstance(doc, fs);

                doc.Open(); // Open Document to write

                Font font8 = FontFactory.GetFont("ARIAL", 10);
                Font headerFont = FontFactory.GetFont("ARIAL", 10);
                headerFont.SetStyle(33);

                // MY TESTING 
                // font8.Color = new BaseColor(Color.Red);

                // Row with table name
                var tableHeader = new PdfPTable(1);
                var tableHeaderCell = new PdfPCell(new Phrase(new Chunk("Aggregated Sales Report", headerFont)));

                tableHeaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHeaderCell.FixedHeight = 30.0f;
                tableHeader.AddCell(tableHeaderCell);
                tableHeader.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                doc.Add(tableHeader);


                if (true)    // Chek if date is empty
                {
                    // Craete instance of the pdf table and set the number of column in that table
                    var pdfPTable = new PdfPTable(5);
                    PdfPCell pdfPCell = null;

                    // Add Header of the pdf table
                    pdfPCell = new PdfPCell(new Phrase(new Chunk("Product", headerFont)));
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPTable.AddCell(pdfPCell);

                    pdfPCell = new PdfPCell(new Phrase(new Chunk("Quantity", headerFont)));
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPTable.AddCell(pdfPCell);

                    pdfPCell = new PdfPCell(new Phrase(new Chunk("Unit Price", headerFont)));
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPTable.AddCell(pdfPCell);


                    pdfPCell = new PdfPCell(new Phrase(new Chunk("Location", headerFont)));
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPTable.AddCell(pdfPCell);

                    pdfPCell = new PdfPCell(new Phrase(new Chunk("Sum", headerFont)));
                    pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfPTable.AddCell(pdfPCell);



                    // How add the data from datatable to pdf table
                    foreach (var cellInfo in dateFromExport)
                    {
                        pdfPCell =
                                new PdfPCell(
                                    new Phrase(
                                        new Chunk(cellInfo, font8)));
                        pdfPTable.AddCell(pdfPCell);
                    }

                    doc.Add(pdfPTable); // add pdf table to the document
                }
            }
            catch (DocumentException docEx)
            {
                // handle pdf document exception if any
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
                // Close document and writer
                doc.Close();
            }
        }
    }
}
