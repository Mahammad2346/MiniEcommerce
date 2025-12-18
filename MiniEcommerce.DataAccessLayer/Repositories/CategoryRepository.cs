using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MiniEcommerceDbContext _context;

        public CategoryRepository(MiniEcommerceDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            return Task.CompletedTask;  
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c=>c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
