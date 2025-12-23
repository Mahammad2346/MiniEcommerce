using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    IQueryable<T> Query();
    void Add(T entity);
    void Delete(T entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
