using System.Linq.Expressions;
namespace MiniEcommerce.Product.API.Repositories;

using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;
public interface IProductRepository
{
	Task<bool> AnyAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken);

	Task<ProductEntity?> FirstOrDefaultAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken);

	Task<IReadOnlyCollection<ProductEntity>> GetProductsAsync(
		int pageNumber,
		int pageSize,
		int? categoryId,
		CancellationToken cancellationToken);

	void Add(ProductEntity product);

	void Delete(ProductEntity product);
}