using Mapster;
using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Product;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ProductNameEmptyException();
            if (dto.Price < 0)
                throw new InvalidProductPriceException(dto.Price);
            var categorExist = await unitOfWork.Categories.AnyAsync(c => c.Id == dto.CategoryId, cancellationToken);
            if (!categorExist)
                throw new CategoryNotFoundException(dto.CategoryId);
            var productExist = await unitOfWork.Products.AnyAsync(p => p.Name == dto.Name && p.CategoryId == dto.CategoryId, cancellationToken);
            if (productExist)
                throw new ProductAlreadyExistsException(dto.Name, dto.CategoryId);

            var product = new Product { Name = dto.Name, Price = dto.Price, CategoryId = dto.CategoryId };
            unitOfWork.Products.Add(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }

        public async Task<ProductDto> DeleteProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(productId);  
            unitOfWork.Products.Delete(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }

        public async Task<ProductDto> GetProduct(int productId, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(productId);
            return product.Adapt<ProductDto>();
        }

        public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(int pageNumber, int pageSize, int? categoryId, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new InvalidPaginationException(pageNumber, pageSize);

            IQueryable<Product> products = unitOfWork.Products.Query();
            if (categoryId is not null)
                products = products.Where(p => p.CategoryId == categoryId);

            var productList = await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return productList.Adapt<IReadOnlyList<ProductDto>>();
        }

        public async Task<ProductDto> UpdateProductAsync(int productId, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ProductNameEmptyException();
            if (dto.Price < 0)
                throw new InvalidProductPriceException(dto.Price);
            var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(productId);
            if (product.CategoryId != dto.CategoryId)
            {
                var categoryExist = await unitOfWork.Categories.AnyAsync(c => c.Id == dto.CategoryId, cancellationToken);
                if (!categoryExist)
                    throw new CategoryNotFoundException(dto.CategoryId);
                product.CategoryId = dto.CategoryId;
            }

            var duplicateExist = await unitOfWork.Products.AnyAsync(p => p.Id != productId && p.Name == dto.Name && p.CategoryId == product.CategoryId, cancellationToken);

            if (duplicateExist)
                throw new ProductAlreadyExistsException(dto.Name, product.CategoryId);

            product.Name = dto.Name;
            product.Price = dto.Price;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Adapt<ProductDto>();
        }
    }
}
