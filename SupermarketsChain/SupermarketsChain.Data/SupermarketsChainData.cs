namespace SupermarketsChain.Data
{
    using System;
    using System.Collections.Generic;

    using SupermarketsChain.Data.Repositories;
    using SupermarketsChain.Data.Repositories.Contracts;
    using SupermarketsChain.Models;

    public class SupermarketsChainData : ISupermarketsChainData
    {
        private readonly ISupermarketsChainDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public SupermarketsChainData(ISupermarketsChainDbContext supermarketsChainDbContext)
        {
            this.context = supermarketsChainDbContext;
        }

        public ISupermarketsChainDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IGenericRepository<Expence> Expences
        {
            get
            {
                return this.GetRepository<Expence>();
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
                return (SalesRepository)this.GetRepository<Sale>();
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

                if (typeof(T).IsAssignableFrom(typeof(Sale)))
                {
                    type = typeof(SalesRepository);
                }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }
    }
}
