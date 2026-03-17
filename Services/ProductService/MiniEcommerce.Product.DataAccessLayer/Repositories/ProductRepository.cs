using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;

public class ProductRepository(MiniEcommerceDbContext dbContext): Repository<ProductEntity>(dbContext), IProductRepository
{
	public async Task<IReadOnlyCollection<ProductEntity>> GetProductsAsync(
		int pageNumber,
		int pageSize,
		int? categoryId,
		CancellationToken cancellationToken)
	{
		IQueryable<ProductEntity> productQuery = DbSet;

		if (categoryId.HasValue)
			productQuery = productQuery.Where(p => p.CategoryId == categoryId.Value);

		return await productQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
}
