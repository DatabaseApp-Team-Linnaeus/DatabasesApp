using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketsChain.Exporters.Xml.Dto
{
    public class XmlDto
    {
        public string VendorName { get; set; }

        public DateTime Date { get; set; }

        public decimal ExpenseValue { get; set; }
    }
}
