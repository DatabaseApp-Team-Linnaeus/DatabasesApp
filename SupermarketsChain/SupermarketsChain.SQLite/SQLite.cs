namespace SupermarketsChain.SQLite
{
    using System;
    using System.Linq;

    using SupermarketsChain.Models;

    public class SQLite
    {
        public static void Main()
        {
            var context = new SqliteDbContext();
            context.Vendors.Add(new Vendor { Name = "Rakiq" });
            context.SaveChanges();
            Console.WriteLine(context.Vendors.FirstOrDefault().Name);
        }
    }
}
