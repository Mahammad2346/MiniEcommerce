using Mapster;
using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Product;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;

namespace MiniEcommerce.BusinessLogicLayer.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private async Task ValidateProductAsync(string name, decimal price, int categoryId, int? excludedProductId, int? currentCategoryId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ProductNameEmptyException();

        if (price < 0)
            throw new InvalidProductPriceException(price);

        if (excludedProductId == null || currentCategoryId != categoryId)
        {
            var categoryExist = await unitOfWork.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
            if(!categoryExist)
                throw new CategoryNotFoundException(categoryId);
        }

        var productExist = await unitOfWork.Products.AnyAsync(p => p.Name == name && p.CategoryId == categoryId && (excludedProductId == null || p.Id != excludedProductId), cancellationToken);

        if (productExist)
            throw new ProductAlreadyExistsException(name, categoryId);
    }
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken)
    {
        await ValidateProductAsync(dto.Name, dto.Price, dto.CategoryId, null, null, cancellationToken);
        var product = dto.Adapt<Product>();
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

        var products = await unitOfWork.Products.GetProductsAsync(pageNumber, pageSize, categoryId, cancellationToken);

        return products.Adapt<IReadOnlyList<ProductDto>>();
    }

    public async Task<ProductDto> UpdateProductAsync(int productId, UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        if(product is null)
            throw new ProductNotFoundException(productId);  
        await ValidateProductAsync(dto.Name, dto.Price, dto.CategoryId, productId, product.CategoryId, cancellationToken);
        dto.Adapt(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Adapt<ProductDto>();
    }
}
