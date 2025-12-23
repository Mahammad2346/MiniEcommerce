using Mapster;
using Microsoft.IdentityModel.Tokens;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<CategoryDto> CreateCategoryAsync(string categoryName, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category Name cant be empty", nameof(categoryName));
            var categoryExist = await unitOfWork.Categories.AnyAsync(c => c.Name == categoryName, cancellationToken);

            if (categoryExist)
                throw new ArgumentException("Category already exist", nameof(categoryName));

            var category = new Category { Name = categoryName };
            unitOfWork.Categories.Add(category);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }

        public async Task<CategoryDto> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category Id must be greater than 0", nameof(categoryId));
            var category = await unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
            var hasProducts = await unitOfWork.Products.AnyAsync(p=>p.CategoryId == categoryId, cancellationToken);
            if (category is null)
                throw new ArgumentException("Category doesnt exist", nameof(categoryId));
            if (hasProducts)
                throw new ArgumentException("Category has at least one Product, cant be removed");
            unitOfWork.Categories.Delete(category);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, string newName, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category Id must be greater than 0", nameof(categoryId));
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category cant be empty", nameof(newName));
            var category = await unitOfWork.Categories.FirstOrDefaultAsync(c=>c.Id == categoryId, cancellationToken);
            if (category is null)
                throw new ArgumentException("Category doesnt exist");
            
            var duplicatedCategory = await unitOfWork.Categories.AnyAsync(c=>c.Id != categoryId &&  c.Name == newName, cancellationToken);

            if (duplicatedCategory)
                throw new ArgumentException("Category already exist", nameof(categoryId));

            category.Name = newName;
            await unitOfWork.SaveChangesAsync(cancellationToken);   
            return category.Adapt<CategoryDto>();
        }
    }
}
