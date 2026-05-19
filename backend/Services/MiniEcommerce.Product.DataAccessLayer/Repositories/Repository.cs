using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;
using System.Linq.Expressions;

namespace MiniEcommerce.Product.DataAccessLayer.Repositories;
public class Repository<T>(MiniEcommerceDbContext dbContext) : IRepository<T>
	where T : class
{
	protected readonly DbSet<T> DbSet = dbContext.Set<T>();

	public void Add(T entity)
	{
		DbSet.Add(entity);
	}

	public void Delete(T entity)
	{
		DbSet.Remove(entity);
	}

	public async Task<bool> AnyAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken)
	{
		return await DbSet.AnyAsync(predicate, cancellationToken);
	}

	public async Task<T?> FirstOrDefaultAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken)
	{
		return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
	}
}
