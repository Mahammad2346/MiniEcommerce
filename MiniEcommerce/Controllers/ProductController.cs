using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Contracts.Dtos;
using MiniEcommerce.Services;
namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductGateway productGateway) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        return await productGateway.GetProductsAsync(pageNumber, pageSize, categoryId, cancellationToken);
	}

    [HttpGet("{productId:int}")]
    public async Task<ProductDto> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
    {
        return await productGateway.GetProductById(productId, cancellationToken);  
	}

    [HttpPost]
    //[Authorize]
    public async Task<ProductDto> CreateProduct([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        return await productGateway.CreateProduct(dto, cancellationToken);   
    }

    [HttpPut("{productId:int}")]
    //[Authorize]
    public async Task<ProductDto> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
    {
        return await productGateway.UpdateProduct(productId, dto, cancellationToken);   
	}

    [HttpDelete("{productId:int}")]
    //[Authorize]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId, CancellationToken cancellationToken)
    {
       var success = await productGateway.DeleteProduct(productId, cancellationToken);

	   if (!success)
			return NotFound();

		return NoContent();
	}
}
