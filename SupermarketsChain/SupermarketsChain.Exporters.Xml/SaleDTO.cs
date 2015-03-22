namespace XmlExporterConsoleClient
{
    using System;

    public class SaleDto
    {
        public SaleDto(DateTime date, decimal saleValue)
        {
            this.Date = date;
            this.SaleValue = saleValue;
        }

        public DateTime Date { get; set; }

        public decimal SaleValue { get; set; }
    }
}
