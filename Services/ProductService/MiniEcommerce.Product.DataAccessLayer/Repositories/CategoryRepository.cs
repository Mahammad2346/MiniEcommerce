using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using System.Linq.Expressions;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;

public class CategoryRepository(MiniEcommerceDbContext dbContext) : ICategoryRepository
{
	public void Add(Category category)
	{
		dbContext.Categories.Add(category);
	}

	public void Delete(Category category)
	{
		dbContext.Categories.Remove(category);
	}

	public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
	{
		return await dbContext.Categories.AnyAsync(predicate, cancellationToken);
	}

	public async Task<Category?> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
	{
		return await dbContext.Categories.FirstOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
	{
		return await dbContext.Categories
			.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
	}
	public async Task<IReadOnlyList<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		return await dbContext.Categories
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
}
