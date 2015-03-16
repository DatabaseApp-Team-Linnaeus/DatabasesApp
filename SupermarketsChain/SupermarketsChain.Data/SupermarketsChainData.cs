namespace SupermarketsChain.Data
{
    using System;
    using System.Collections.Generic;

    using SupermarketsChain.Data.Repositories;
    using SupermarketsChain.Data.Repositories.Contracts;
    using SupermarketsChain.Models;

    public class SupermarketsChainData : ISupermarketsChainData
    {
        private ISupermarketsChainDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public SupermarketsChainData(ISupermarketsChainDbContext supermarketsChainDbContext)
        {
            this.context = supermarketsChainDbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public ISupermarketsChainDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IGenericRepository<Expense> Expenses
        {
            get
            {
                return this.GetRepository<Expense>();
            }
        }

        public IGenericRepository<Measure> Measures
        {
            get
            {
                return this.GetRepository<Measure>();
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IGenericRepository<ProductTax> ProductTaxes
        {
            get
            {
                return this.GetRepository<ProductTax>();
            }
        }

        public IGenericRepository<Supermarket> Supermarkets
        {
            get
            {
                return this.GetRepository<Supermarket>();
            }
        }

        public IGenericRepository<Town> Towns
        {
            get
            {
                return this.GetRepository<Town>();
            }
        }

        public ISalesRepository Sales
        {
            get
            {
                return (ISalesRepository)this.GetRepository<Sale>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                if (typeof(Sale).IsAssignableFrom(typeof(T)))
                {
                    type = typeof(SalesRepository);
                }

                var newRepo = Activator.CreateInstance(type, this.context);
                this.repositories.Add(typeof(T), newRepo);
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }
    }
}
