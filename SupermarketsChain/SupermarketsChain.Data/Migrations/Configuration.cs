namespace SupermarketsChain.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using SupermarketsChain.Data.Contexts;
    using SupermarketsChain.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SupermarketsChainDbContext>
    {
        private readonly Random random = new Random(0);

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SupermarketsChainDbContext context)
        {
            var towns = this.SeedTowns(context);
            var supermarkets = this.SeedSupermarkets(context, towns);
            var vendors = this.SeedVendors(context);
            var measures = this.SeedMeasures(context);
            var products = this.SeedProducts(context, vendors, measures);
            this.SeedSales(context, products, supermarkets);
        }

        private void SeedSales(SupermarketsChainDbContext context, IList<Product> products, IList<Supermarket> supermarkets)
        {
            var sales = new List<Sale>();
            for (int i = 0; i < 100; i++)
            {
                var unitPrice = this.random.Next(1000);
                var quantity = this.random.Next(1000);
                sales.Add(new Sale
                              {
                                  Product = products[this.random.Next(products.Count)],
                                  Supermarket = supermarkets[this.random.Next(supermarkets.Count)],
                                  SoldDate = this.RandomDay(),
                                  PricePerUnit = unitPrice,
                                  Quantity = quantity,
                                  SaleCost = unitPrice * quantity
                              });
            }

            foreach (var sale in sales)
            {
                context.Sales.Add(sale);
            }

            context.SaveChanges();
        }

        private IList<Measure> SeedMeasures(SupermarketsChainDbContext context)
        {
            var measures = new List<Measure>
                            {
                                new Measure { Name = "Liter" },
                                new Measure { Name = "Unit" },
                                new Measure { Name = "Kilograms" },
                                new Measure { Name = "Grams" }
                            };
            foreach (var measure in measures)
            {
                context.Measures.AddOrUpdate(measure);
            }

            context.SaveChanges();

            return measures;
        }

        private IList<Product> SeedProducts(SupermarketsChainDbContext context, IList<Vendor> vendors, IList<Measure> measures)
        {
            var products = new List<Product>
                                   {
                                       new Product
                                           {
                                               Name = "Laptop",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Tablet",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Smartphone",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Glass",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Monitor",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Visual Studio",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Energy Drink",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Banana",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Potato",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Lan Cable",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Shirt",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Beer",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Vodka",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Wisky",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           },
                                       new Product
                                           {
                                               Name = "Coffee",
                                               Vendor = vendors[this.random.Next(vendors.Count)],
                                               Measure = measures[this.random.Next(measures.Count)]
                                           }
                                   };
            foreach (var product in products)
            {
                context.Products.AddOrUpdate(product);
            }

            context.SaveChanges();

            return products;
        }

        private IList<Vendor> SeedVendors(SupermarketsChainDbContext context)
        {
            var vendors = new List<Vendor>
                            {
                                new Vendor { Name = "Bosh" },
                                new Vendor { Name = "Simens" },
                                new Vendor { Name = "Toshiba" },
                                new Vendor { Name = "FF" },
                                new Vendor { Name = "K-Classics" },
                                new Vendor { Name = "Clever" }
                            };
            foreach (var vendor in vendors)
            {
                context.Vendors.AddOrUpdate(vendor);
            }

            context.SaveChanges();

            return vendors;
        }

        private IList<Supermarket> SeedSupermarkets(SupermarketsChainDbContext context, IList<Town> towns)
        {
            var supermarkets = new List<Supermarket>
                                   {
                                       new Supermarket
                                           {
                                               Name = "Billa",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Billa",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Fantastico",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "T-Market",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "MyShop",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Dar",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Kal",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Kaufland",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "MyShop",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Kaufland",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Lidl",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Plus",
                                               Town = towns[this.random.Next(towns.Count)]
                                           },
                                       new Supermarket
                                           {
                                               Name = "Tempo",
                                               Town = towns[this.random.Next(towns.Count)]
                                           }
                                   };
            foreach (var supermarket in supermarkets)
            {
                context.Supermarkets.AddOrUpdate(supermarket);
            }

            context.SaveChanges();

            return supermarkets;
        }

        private IList<Town> SeedTowns(SupermarketsChainDbContext context)
        {
            var towns = new List<Town>
                            {
                                new Town { Name = "Sofia" },
                                new Town { Name = "Varna" },
                                new Town { Name = "Burgas" },
                                new Town { Name = "Plovdiv" },
                                new Town { Name = "Ruse" },
                                new Town { Name = "Pernik" }
                            };
            foreach (var town in towns)
            {
                context.Towns.AddOrUpdate(town);
            }

            context.SaveChanges();

            return towns;
        }

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);

            int range = (DateTime.Today - start).Days;
            return start.AddDays(this.random.Next(range));
        }
    }
}
