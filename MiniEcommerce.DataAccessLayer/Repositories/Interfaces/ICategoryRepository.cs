using MiniEcommerce.BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Add(Category category);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
