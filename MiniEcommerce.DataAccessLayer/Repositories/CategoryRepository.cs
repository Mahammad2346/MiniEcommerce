using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories
{
    public class CategoryRepository(MiniEcommerceDbContext context) : ICategoryRepository
    {
        private readonly MiniEcommerceDbContext _context = context;
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
        public async Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c=>c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == id, cancellationToken);
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
