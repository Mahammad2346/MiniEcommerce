using Microsoft.EntityFrameworkCore;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
}
