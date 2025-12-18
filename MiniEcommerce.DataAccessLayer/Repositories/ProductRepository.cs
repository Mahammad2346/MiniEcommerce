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
    public class ProductRepository(MiniEcommerceDbContext context) : IProductRepository
    {
        private readonly MiniEcommerceDbContext _context = context;    
        public void Add(Product product)
            {
                _context.Products.Add(product);
            }
        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber < 1)
                pageNumber = 1;

           return await _context.Products
                .AsNoTracking()
                .OrderBy(p=>p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);    
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}