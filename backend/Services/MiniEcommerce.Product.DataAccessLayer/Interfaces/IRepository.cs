using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MiniEcommerce.Product.DataAccessLayer.Interfaces
{
	public interface IRepository<T> where T : class
	{
		void Add(T entity);

		void Delete(T entity);

		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
	}
}
