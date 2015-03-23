namespace SupermarketsChain.Exportes.PDF
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using SupermarketsChain.Data;
    using SupermarketsChain.Infrastructure.Infrastructure;

    internal class ExporterPDF
    {
        private static void Main()
        {

            var data = ObjectFactory.Get<ISupermarketsChainData>();
            var obj =
                data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now)
                    .Select(s => new
                                     {
                                         s.Product,
                                         s.Quantity,
                                         s.PricePerUnit,
                                         s.Supermarket, 
                                         s.SaleCost
                                     });
 
            var dateFromExport = new List<string>();

            foreach (var sales in obj)
            {
                dateFromExport.Add(" \"" + sales.Product.Name.ToString() + "\" ");
                dateFromExport.Add(sales.Quantity.ToString());
                dateFromExport.Add(sales.PricePerUnit.ToString());
                dateFromExport.Add(" \"" + sales.Supermarket.Name.ToString() + "\" ");
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

                // Row with table name
                var tableHeader = new PdfPTable(1);
                tableHeader.TotalWidth = 500f;
                tableHeader.LockedWidth = true;

                var tableHeaderCell = new PdfPCell(new Phrase(new Chunk("Aggregated Sales Report", headerFont)));

                tableHeaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHeaderCell.FixedHeight = 30.0f;
                tableHeader.AddCell(tableHeaderCell);
                tableHeader.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                tableHeaderCell = new PdfPCell(new Phrase(
                    new Chunk(
                        " Date: " + DateTime.Today.ToString("dd-MM-yyyy"),
                        font8)));
                tableHeader.AddCell(tableHeaderCell);

                doc.Add(tableHeader);

                if (true)    // Chek if date is empty
                {
                    // Craete instance of the pdf table and set the number of column in that table
                    var pdfPTable = new PdfPTable(5);
                    PdfPCell pdfPCell = null;

                    pdfPTable.TotalWidth = 500f;
                    pdfPTable.LockedWidth = true;
                    var widths = new[] { 150f, 50f, 50f, 200f, 50f };
                    pdfPTable.SetWidths(widths);

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

                    doc.Add(pdfPTable); // add pdf main content

                    double totalSum = 0;
                    for (int i = 4; i < dateFromExport.Count; i += 5)
                    {
                        totalSum = totalSum + Convert.ToDouble(dateFromExport[i]);
                    }

                    // Table footer row. (total sum)
                    var tableFooter = new PdfPTable(2);
                    tableFooter.TotalWidth = 500f;
                    tableFooter.LockedWidth = true;
                    widths = new[] { 450f, 50f };
                    tableFooter.SetWidths(widths);

                    var tableFooterCell = new PdfPCell(new Phrase(new Chunk("Total sum", headerFont)));
                    tableFooter.AddCell(tableFooterCell);

                    tableFooterCell = new PdfPCell(new Phrase(new Chunk(totalSum.ToString(), headerFont)));
                    tableFooter.AddCell(tableFooterCell);

                    doc.Add(tableFooter);
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
