using MiniEcommerce.BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Add(Product product);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
