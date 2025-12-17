using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MiniEcommerce.DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiniEcommerceDbContext _context;
        public ProductRepository(MiniEcommerceDbContext context)
        {
                _context = context; 
        }
        public void Add(Product product)
        {
            _context.Products.Add(product); 
        }

        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p=>p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
           return _context.Products.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
