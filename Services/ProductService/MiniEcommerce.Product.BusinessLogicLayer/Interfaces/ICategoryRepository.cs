using System.Linq.Expressions;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
namespace MiniEcommerce.Product.API.Interfaces;

public interface ICategoryRepository
{
	Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);

	Task<Category?> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
	Task<IReadOnlyList<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
	Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);

	void Add(Category category);

	void Delete(Category category);
}