using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.DataAccessLayer.Entities;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;

public class CategoryRepository(MiniEcommerceDbContext dbContext): Repository<Category>(dbContext), ICategoryRepository
{
	public async Task<Category?> GetByNameAsync(
		string name,
		CancellationToken cancellationToken)
	{
		return await DbSet
			.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
	}

	public async Task<IReadOnlyList<Category>> GetAllAsync(
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken)
	{
		return await DbSet
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
}
