using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.DataAccessLayer;

namespace MiniEcommerce.Product.API;

public class UnitOfWork(
	MiniEcommerceDbContext dbContext,
	IProductRepository productRepository,
	ICategoryRepository categoryRepository) : IUnitOfWork
{
	public IProductRepository Products { get; } = productRepository;

	public ICategoryRepository Categories { get; } = categoryRepository;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		=> dbContext.SaveChangesAsync(cancellationToken);
}
