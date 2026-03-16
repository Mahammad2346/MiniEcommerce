using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.DataAccessLayer;
using MiniEcommerce.Product.DataAccessLayer.Repositories;

namespace MiniEcommerce.Product.API;

public class UnitOfWork : IUnitOfWork
{
	private readonly MiniEcommerceDbContext _dbContext;

	private readonly Lazy<IProductRepository> _products;
	private readonly Lazy<ICategoryRepository> _categories;

	public UnitOfWork(MiniEcommerceDbContext dbContext)
	{
		_dbContext = dbContext;

		_products = new Lazy<IProductRepository>(() => new ProductRepository(_dbContext));
		_categories = new Lazy<ICategoryRepository>(() => new CategoryRepository(_dbContext));
	}

	public IProductRepository Products => _products.Value;

	public ICategoryRepository Categories => _categories.Value;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		=> _dbContext.SaveChangesAsync(cancellationToken);
}