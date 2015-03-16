﻿namespace SupermarketsChain.Data
 {
     using System.Data.Entity;
     using System.Data.Entity.Infrastructure;

     using SupermarketsChain.Models;

     public interface ISupermarketsChainDbContext
     {
         IDbSet<Expence> Expences { get; set; }

         IDbSet<Measure> Measures { get; set; }

         IDbSet<Product> Products { get; set; }

         IDbSet<Sale> Sales { get; set; }

         IDbSet<ProductTax> ProductTaxes { get; set; }

         IDbSet<Supermarket> Supermarkets { get; set; }

         IDbSet<Town> Towns { get; set; }

         IDbSet<Vendor> Vendors { get; set; }

         IDbSet<TEntity> Set<TEntity>() where TEntity : class;

         DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

         int SaveChanges();
     }
 }