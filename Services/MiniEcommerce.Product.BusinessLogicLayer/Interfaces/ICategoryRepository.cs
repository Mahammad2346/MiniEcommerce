using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using MiniEcommerce.Product.BusinessLogicLayer.Interfaces;

namespace MiniEcommerce.Product.API.Interfaces;

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
