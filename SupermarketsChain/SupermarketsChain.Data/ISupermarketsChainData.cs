﻿namespace SupermarketsChain.Data
{
    using SupermarketsChain.Data.Repositories.Contracts;
    using SupermarketsChain.Models;

    public interface ISupermarketsChainData
    {
        ISupermarketsChainDbContext Context { get; }

        IGenericRepository<Expense> Expenses { get; }

        IGenericRepository<Measure> Measures { get; }

        IGenericRepository<Product> Products { get; }

        IGenericRepository<ProductTax> ProductTaxes { get; }

        IGenericRepository<Supermarket> Supermarkets { get; }

        IGenericRepository<Town> Towns { get; }

        ISalesRepository Sales { get; }
        
        int SaveChanges();
    }
}
