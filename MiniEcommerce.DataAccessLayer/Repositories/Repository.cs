using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerce.DataAccessLayer.Repositories;
public class Repository<T>(MiniEcommerceDbContext context) : IRepository<T> where T : class
{
        protected readonly DbSet<T> DbSet = context.Set<T>();

        public void Add(T entity)
        {
            DbSet.Add(entity);  
        }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            return await DbSet
                .AsNoTracking()
                .Skip((pageNumber-1)  * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }
}
