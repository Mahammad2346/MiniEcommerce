using MiniEcommerce.Product.DataAccessLayer.Entities;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;
using ProductEntity = MiniEcommerce.Product.DataAccessLayer.Entities.Product;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
	Task<IReadOnlyCollection<ProductEntity>> GetProductsAsync(
		int pageNumber,
		int pageSize,
		int? categoryId,
		CancellationToken cancellationToken);
}
