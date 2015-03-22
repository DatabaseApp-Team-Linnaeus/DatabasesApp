namespace SupermarketsChain.SQLite
{
    using SupermarketsChain.Data.Contexts;
    using SupermarketsChain.Models;

    public class SQLite
    {
        public static void Main()
        {
            var context = new SqliteDbContext();
            context.Measures.Add(new Measure { Name = "gram" });
            context.SaveChanges();
        }
    }
}
