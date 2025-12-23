using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(
            string categoryName,
            CancellationToken cancellationToken);
        Task<CategoryDto> UpdateCategoryAsync(
            int categoryId,
            string newName,
            CancellationToken cancellationToken);
        Task<CategoryDto> DeleteCategoryAsync(
            int categoryId,
            CancellationToken cancellationToken);
    }   
}
