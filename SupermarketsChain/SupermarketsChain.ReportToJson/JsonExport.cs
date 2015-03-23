namespace SupermarketsChain.ReportToJson
{
    using System;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    public static class JsonExport
    {
        public static void Main()
        {
            
        }

        public static void Export(DateTime startDate, DateTime endDate)
        {
            var data = ObjectFactory.Get<ISupermarketsChainData>();

            var obj =
                data.Sales.GetAllByDateInterval(startDate, endDate)
                 .GroupBy(y => new { y.Product, y.Product.Vendor })
                    .Select(
                        x => new JsonReportDTO
                                 {
                                     ProductId = x.Key.Product.Id,
                                     ProductName = x.Key.Product.Name,
                                     VendorName = x.Key.Product.Vendor.Name,
                                     TotalQuantity = x.Sum(y => y.Quantity),
                                     TotalCost = x.Sum(y => y.Quantity * y.PricePerUnit),
                                 });

            foreach (var file in obj)
            {
                string json = JsonConvert.SerializeObject(file, Formatting.Indented);
                File.WriteAllText(@"D:\" + file.ProductId + ".json", json);
            }
        }
    }
}
