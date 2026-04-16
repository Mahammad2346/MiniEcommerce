using MiniEcommerce.Product.DataAccessLayer;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;
using MiniEcommerce.Product.DataAccessLayer.Repositories;

namespace MiniEcommerce.Product.DataAccessLayer;
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
