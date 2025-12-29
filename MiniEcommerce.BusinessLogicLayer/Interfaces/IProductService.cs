using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken);

    Task<ProductDto> UpdateProductAsync(int productId, UpdateProductDto dto, CancellationToken cancellationToken);
    Task<ProductDto> DeleteProductAsync(int productId, CancellationToken cancellationToken);
    Task<ProductDto> GetProduct(int productId, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductDto>> GetProductsAsync(
        int pageNumber,
        int pageSize,
        int? categoryId,
        CancellationToken cancellationToken);   
}
