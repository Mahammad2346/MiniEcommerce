using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer
{
    public class UnitOfWork(MiniEcommerceDbContext dbContext) : IUnitOfWork
    {
        private readonly IRepository<Product> _productRepository = new Repository<Product>(dbContext);
        private readonly IRepository<Category> _categoryRepository = new Repository<Category>(dbContext);

        public IRepository<Product> Products => _productRepository;

        public IRepository<Category> Categories => _categoryRepository;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return dbContext.SaveChangesAsync(cancellationToken); ;
        }
    }
}
