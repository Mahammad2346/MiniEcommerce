using MiniEcommerce.BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int pageSize);

        Task<Category?> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task SaveAsync();
    }
}
