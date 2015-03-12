namespace SupermarketsChain.Data.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T>
    {
        IQueryable<T> All();

        IQueryable<T> Search(Expression<Func<T, bool>> conditions);

        T GetById(object id);

        void Add(T entity);

        T Delete(T entity);

        void Update(T entity);

        T DeleteById(object id);

        void SaveChanges();
    }
}
