using Microsoft.EntityFrameworkCore;
using ProductEntity = MiniEcommerce.Product.DataAccessLayer.Entities.Product;

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
