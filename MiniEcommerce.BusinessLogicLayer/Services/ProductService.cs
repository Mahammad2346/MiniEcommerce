using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using Mapster;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ProductDto> CreateProductAsync(string name, decimal price, int categoryId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Product name cant be empty", nameof(name));
            if (price < 0)
                throw new ArgumentOutOfRangeException("Product price must be greater than zero");
            var categorExist = await unitOfWork.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
            if (!categorExist)
                throw new ArgumentException("Category does not exist");
            var productExist = await unitOfWork.Products.AnyAsync(p => p.Name == name && p.CategoryId == categoryId, cancellationToken);
            if (productExist)
                throw new ArgumentException("A product with the same name already exist in this category");

            var product = new Product { Name = name, Price = price, CategoryId = categoryId };
            unitOfWork.Products.Add(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }

        public async Task<ProductDto> DeleteProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ArgumentException("Product doesnt exist", nameof(productId));
            unitOfWork.Products.Delete(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }

        public async Task<ProductDto> GetProduct(int productId, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ArgumentException("Not found", nameof(product));
            return product.Adapt<ProductDto>();
        }

        public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(int pageNumber, int pageSize, int? categoryId, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            IQueryable<Product> products = unitOfWork.Products.Query();
            if (categoryId is not null)
                products = products.Where(p => p.CategoryId == categoryId);

            var productList = await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return productList.Adapt<IReadOnlyList<ProductDto>>();
        }

        public async Task<ProductDto> UpdateProductAsync(int productId, string newName, decimal newPrice, int newCategoryId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("Product Name cant be empty", nameof(newName));
            if (newPrice < 0)
                throw new ArgumentException("New Price must be greater than zero");
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ArgumentException("Product does not exist", nameof(product));
            if (product.CategoryId != newCategoryId)
            {
                var categoryExist = await unitOfWork.Categories.AnyAsync(c => c.Id == newCategoryId, cancellationToken);
                if (!categoryExist)
                    throw new ArgumentException("Category doesnt exist");
                product.CategoryId = newCategoryId;
            }

            var duplicateExist = await unitOfWork.Products.AnyAsync(p => p.Id != productId && p.Name == newName && p.CategoryId == product.CategoryId, cancellationToken);

            if (duplicateExist)
                throw new ArgumentException("A product with same name already exist in this category");

            product.Name = newName;
            product.Price = newPrice;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }
    }
}
