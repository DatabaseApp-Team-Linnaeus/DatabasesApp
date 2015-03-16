namespace SupermarketsChain.ReportToJson
{
    using System;
    using System.Linq;

    using Newtonsoft.Json;

    using SupermarketsChain.ConsoleClient.Infrastructure;
    using SupermarketsChain.Data;

    public class JsonTest
    {
        public static void Main()
        {
            var data = ObjectFactory.Get<ISupermarketsChainData>();

            var obj =
                data.Sales.GetAllByDateInterval(new DateTime(1950, 1, 1), DateTime.Now)
                 .GroupBy(y => new { y.Product, y.Product.Vendor })
                    .Select(
                        x => new TestDTO
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
                System.IO.File.WriteAllText(@"D:\" + file.ProductId + ".json", json);
            }
        }
    }
}
