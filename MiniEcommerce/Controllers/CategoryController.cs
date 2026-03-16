using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Authorization;
using MiniEcommerce.Product.Contracts;
using MiniEcommerce.Services;


namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ProductGateway productGateway) : ControllerBase
{
	[HttpGet]
	//[Authorize(Policy = AuthorizationPolicies.ReadCategories)]
	public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(
	[FromQuery] int pageNumber = 1,
	[FromQuery] int pageSize = 10,
	CancellationToken cancellationToken = default)
	{
		return await productGateway.GetCategories(pageNumber, pageSize, cancellationToken);
	}

	[HttpGet("{categoryId:int}")]
	//[Authorize(Policy = AuthorizationPolicies.ReadCategories)]
	public async Task<CategoryDto> GetCategoryById([FromRoute] int categoryId, CancellationToken cancellationToken)
	{
		return await productGateway.GetCategoryById(categoryId, cancellationToken);
	}

	[HttpPost]
	//[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<CategoryDto> CreateCategory([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken = default)
	{
		return await productGateway.CreateCategory(dto, cancellationToken);
	}

	[HttpPut("{categoryId:int}")]
	//[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<CategoryDto> UpdateCategory([FromRoute] int categoryId, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
	{
		return await productGateway.UpdateCategory(categoryId, dto, cancellationToken);
	}

	[HttpDelete("{categoryId:int}")]
	//[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
	{
		var success = await productGateway.DeleteCategory(categoryId, cancellationToken);

		if (!success)
			return NotFound();

		return NoContent();
	}
}