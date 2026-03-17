using MiniEcommerce.Product.BusinessLogicLayer.Interfaces;
using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;

namespace MiniEcommerce.Product.API.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
	Task<IReadOnlyCollection<ProductEntity>> GetProductsAsync(
		int pageNumber,
		int pageSize,
		int? categoryId,
		CancellationToken cancellationToken);
}
