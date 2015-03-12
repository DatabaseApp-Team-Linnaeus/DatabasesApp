namespace SupermarketsChain.Data.Repositories.Contracts
{
    using System;
    using System.Linq;

    using SupermarketsChain.Models;

    public interface ISalesRepository : IGenericRepository<Sale>
    {
        IQueryable<Sale> GetAllByDateInterval(DateTime startDate, DateTime endDate);
    }
}
