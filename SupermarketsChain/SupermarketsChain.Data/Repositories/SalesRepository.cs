namespace SupermarketsChain.Data.Repositories
{
    using System;
    using System.Linq;

    using SupermarketsChain.Data.Repositories.Contracts;
    using SupermarketsChain.Models;

    internal class SalesRepository : GenericRepository<Sale>, ISalesRepository
    {
        public SalesRepository(ISupermarketsChainDbContext supermarketsChainDbContext)
            : base(supermarketsChainDbContext)
        {
        }

        public IQueryable<Sale> GetAllByDateInterval(DateTime startDate, DateTime endDate)
        {
            return this.Context.Sales.Where(x => x.SoldDate >= startDate && x.SoldDate <= endDate);
        }
    }
}
