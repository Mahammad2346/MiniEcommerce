using Mapster;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        private async Task<Category> GetCategoryOrThrowAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
            return category;
        }
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new CategoryNameEmptyException();
            var categoryExist = await unitOfWork.Categories.AnyAsync(c => c.Name == dto.Name, cancellationToken);

            if (categoryExist)
                throw new CategoryAlreadyExistsException(dto.Name);

            var category = new Category { Name = dto.Name };
            unitOfWork.Categories.Add(category);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }

        public async Task<CategoryDto> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
                throw new InvalidCategoryIdException(categoryId);
            var category = await GetCategoryOrThrowAsync(categoryId, cancellationToken);
            var hasProducts = await unitOfWork.Products.AnyAsync(p=>p.CategoryId == categoryId, cancellationToken);
   
            if (hasProducts)
                throw new CategoryHasProductsException(categoryId);
            unitOfWork.Categories.Delete(category);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, UpdateCategoryDto dto, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
                throw new InvalidCategoryIdException(categoryId);
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new CategoryNameEmptyException();
            var category = await GetCategoryOrThrowAsync(categoryId, cancellationToken);
           
            
            var duplicatedCategory = await unitOfWork.Categories.AnyAsync(c=>c.Id != categoryId && c.Name == dto.Name, cancellationToken);

            if (duplicatedCategory)
                throw new CategoryAlreadyExistsException(dto.Name);

            category.Name = dto.Name;
            await unitOfWork.SaveChangesAsync(cancellationToken);   
            return category.Adapt<CategoryDto>();
        }
    }
}
