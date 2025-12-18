using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniEcommerce.DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiniEcommerceDbContext _context;

        public ProductRepository(MiniEcommerceDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Product product)
            {
                _context.Products.Add(product);
                return Task.CompletedTask;  
            }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
           return await _context.Products
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);    
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
