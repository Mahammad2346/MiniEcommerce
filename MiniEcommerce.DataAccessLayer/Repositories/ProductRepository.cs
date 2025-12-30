using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories;


public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MiniEcommerceDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyCollection<Product>> GetProductsAsync(int pageNumber, int pageSize, int? categoryId, CancellationToken cancellationToken)
    {
        IQueryable<Product> productQuery = DbSet;
        if(categoryId.HasValue)
            productQuery = productQuery.Where(p=>p.CategoryId == categoryId.Value);

        return await productQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
