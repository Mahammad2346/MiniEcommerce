using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;
using MiniEcommerce.Product.API.Repositories;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly MiniEcommerceDbContext _dbContext;

	public ProductRepository(MiniEcommerceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Add(ProductEntity product)
	{
		_dbContext.Products.Add(product);
	}

	public void Delete(ProductEntity product)
	{
		_dbContext.Products.Remove(product);
	}

	public async Task<bool> AnyAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken)
	{
		return await _dbContext.Products.AnyAsync(predicate, cancellationToken);
	}

	public async Task<ProductEntity?> FirstOrDefaultAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken)
	{
		return await _dbContext.Products.FirstOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<IReadOnlyCollection<ProductEntity>> GetProductsAsync(
		int pageNumber,
		int pageSize,
		int? categoryId,
		CancellationToken cancellationToken)
	{
		IQueryable<ProductEntity> productQuery = _dbContext.Products;

		if (categoryId.HasValue)
			productQuery = productQuery.Where(p => p.CategoryId == categoryId.Value);

		return await productQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
}