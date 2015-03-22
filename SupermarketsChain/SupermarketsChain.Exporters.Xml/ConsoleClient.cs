// TODO: Change the namespace or the project name/location in solution?
// TODO: refactor into a method so it can be run easily from other places

namespace SupermarketsChain.Exporters.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    using XmlExporterConsoleClient;
    
    public static class ConsoleClient
    {
        private const string DateFormat = "dd-MMM-yyyy";
        private const string OutputFolderPath = @"../../../Export/";
        private const string FileName = "sales-report.xml";

        public static void Main(string[] args)
        {

            var context = ObjectFactory.Get<ISupermarketsChainData>();
            var startDate = DateTime.Parse("1950-01-01");
            var endDate = DateTime.Now;

            var vendorsAndSales = context.Products.All()
                .Select(x => new
                {
                    x.Vendor.Name,
                    Sales = x.Sales.Where(y => y.SoldDate >= startDate && y.SoldDate <= endDate)
                })
                .OrderBy(x => x.Name);

            var salesPerVendor = new SortedDictionary<string, List<SaleDto>>();
            
            foreach (var vendor in vendorsAndSales)
            {
                var key = vendor.Name;
                if (!salesPerVendor.ContainsKey(key))
                {
                    salesPerVendor[key] = new List<SaleDto>();

                }

                foreach (var sale in vendor.Sales)
                {
                    salesPerVendor[key].Add(new SaleDto(sale.SoldDate, sale.SaleCost));
                }
            }

            CreateXmlFromSortedDictionary(salesPerVendor, OutputFolderPath, FileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesPerVendor">Sorted by Key SortedDictionary with list of sales per vendor</param>
        /// <param name="pathToFolder">Path to output folder </param>
        /// <param name="fileName">FileName</param>
        /// <exception cref="IOException">No space on disk or no access to parent folder</exception>
        private static void CreateXmlFromSortedDictionary(SortedDictionary<string, List<SaleDto>> salesPerVendor, string pathToFolder, string fileName)
        {
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }


            var doc = new XElement("sales");
            foreach (var item in salesPerVendor) //.OrderBy(v => v.Key))
            {
                var vendorXml = new XElement("sale", new XAttribute("vendor", item.Key));
                foreach (var sale in item.Value.OrderBy(d => d.Date))
                {
                    var saleXml = new XElement(
                        "summary",
                        new XAttribute("date", sale.Date.ToString(DateFormat, CultureInfo.InvariantCulture)),
                        new XAttribute("total-sum", sale.SaleValue));
                    vendorXml.Add(saleXml);
                }
                doc.Add(vendorXml);
            }

            var fullPathToFolder = Path.GetFullPath(pathToFolder);
            var fullPathToFile = Path.Combine(fullPathToFolder, fileName);
            try
            {
                doc.Save(fullPathToFile);
                Console.WriteLine("Sales report generated to:\n{0}", fullPathToFile);

            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot write file: " + fullPathToFile);
                Console.WriteLine("see stacktrace" + e.StackTrace);
            }
        }
    }
}
