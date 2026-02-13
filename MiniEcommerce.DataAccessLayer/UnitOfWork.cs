using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer;

public class UnitOfWork(MiniEcommerceDbContext dbContext) : IUnitOfWork
{
    private readonly Lazy<IProductRepository> _products = new(() => new ProductRepository(dbContext));

    private readonly Lazy<IRepository<Category>> _categories = new(() => new Repository<Category>(dbContext));

    public IProductRepository Products => _products.Value;
    public IRepository<Category> Categories => _categories.Value;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);
}
