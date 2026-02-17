using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Interfaces;

namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        return await productService.GetProductsAsync(pageNumber, pageSize, categoryId, cancellationToken);
    }

    [HttpGet("{productId:int}")]
    public async Task<ProductDto> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
    {
        return await productService.GetProduct(productId, cancellationToken);
    }

    [HttpPost]
    [Authorize]
    public async Task<ProductDto> CreateProduct( [FromBody] CreateProductDto dto,CancellationToken cancellationToken)
    {
        return await productService.CreateProductAsync(dto, cancellationToken);  
    }

    [HttpPut("{productId:int}")]
    [Authorize]
    public async Task<ProductDto> UpdateProduct( [FromRoute] int productId, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
    {
        return await productService.UpdateProductAsync(productId, dto, cancellationToken);
    }

    [HttpDelete("{productId:int}")]
    [Authorize]
    public async Task<ProductDto> DeleteProduct([FromRoute] int productId, CancellationToken cancellationToken)
    {
        return await productService.DeleteProductAsync(productId, cancellationToken);
    }
}
