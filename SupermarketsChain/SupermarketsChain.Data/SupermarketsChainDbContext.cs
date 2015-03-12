namespace SupermarketsChain.Data
{
    using System.Data.Entity;

    using SupermarketsChain.Models;

    public class SupermarketsChainDbContext : DbContext, ISupermarketsChainDbContext
    {
        public SupermarketsChainDbContext()
            : base("SupermarketsChain")
        {
        }

        public IDbSet<Expence> Expences { get; set; }

        public IDbSet<Measure> Measures { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<ProductTax> ProductTaxes { get; set; }

        public IDbSet<Supermarket> Supermarkets { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Vendor> Vernors { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
