using MiniEcommerce.BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);

        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task SaveAsync();
    }
}
