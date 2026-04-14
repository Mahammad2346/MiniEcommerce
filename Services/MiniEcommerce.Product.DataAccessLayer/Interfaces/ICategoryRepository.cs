using MiniEcommerce.Product.DataAccessLayer.Entities;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;

namespace MiniEcommerce.Product.DataAccessLayer.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
	Task<IReadOnlyList<Category>> GetAllAsync(
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken);

	Task<Category?> GetByNameAsync(
		string name,
		CancellationToken cancellationToken);
}
