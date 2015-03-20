namespace SupermarketsChain.Exporters.Xml.Dto
{
    using System;
    using System.Collections.Generic;

    public class DtoSales
    {
        
       

        public DateTime Date { get; set; }

        public decimal ExpenseValue { get; set; }
    }

    public class XmlVendor
    {
        public string VendorName { get; set; }

        public List<DtoSales> Sales { get; set; }
    }
}
