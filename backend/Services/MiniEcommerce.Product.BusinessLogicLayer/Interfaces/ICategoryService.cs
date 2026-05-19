using MiniEcommerce.Product.Contracts;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Interfaces;
public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<CategoryDto> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken);
	Task<CategoryDto> UpdateCategoryAsync(int categoryId, UpdateCategoryDto dto, CancellationToken cancellationToken);
    Task<CategoryDto> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);
}   
