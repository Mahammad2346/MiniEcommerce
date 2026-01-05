using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Responses;

namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        var products = await productService.GetProductsAsync(pageNumber, pageSize, categoryId, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProductDto>>.Ok(products));
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
    {
        var product = await productService.GetProduct(productId, cancellationToken);
        return Ok(ApiResponse<ProductDto>.Ok(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct( [FromBody] CreateProductDto dto,CancellationToken cancellationToken)
    {
        var createdProduct = await productService.CreateProductAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, ApiResponse<ProductDto>.Ok(createdProduct));
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> UpdateProduct( [FromRoute] int productId, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var updatedProduct = await productService.UpdateProductAsync(productId, dto, cancellationToken);
        return Ok(ApiResponse<ProductDto>.Ok(updatedProduct));
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId, CancellationToken cancellationToken)
    {
        await productService.DeleteProductAsync(productId, cancellationToken);
        return NoContent();
    }
}
