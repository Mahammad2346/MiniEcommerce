using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Authorization;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.Services;

namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ProductGateway productGateway) : ControllerBase
{
	[HttpGet]
	[Authorize(Policy = AuthorizationPolicies.ReadCategories)]
	public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(
	[FromQuery] int pageNumber = 1,
	[FromQuery] int pageSize = 10,
	CancellationToken cancellationToken = default)
	{
		return await productGateway.GetCategories(pageNumber, pageSize, cancellationToken);
	}

	[HttpGet("{categoryId:int}")]
	[Authorize(Policy = AuthorizationPolicies.ReadCategories)]
	public async Task<CategoryDto> GetCategoryById([FromRoute] int categoryId, CancellationToken cancellationToken)
	{
		return await productGateway.GetCategoryById(categoryId, cancellationToken);
	}

	[HttpPost]
	[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken = default)
	{
		var createdCategory = await productGateway.CreateCategory(dto, cancellationToken);

		return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id }, createdCategory);
	}

	[HttpPut("{categoryId:int}")]
	[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<CategoryDto> UpdateCategory([FromRoute] int categoryId, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
	{
		return await productGateway.UpdateCategory(categoryId, dto, cancellationToken);
	}

	[HttpDelete("{categoryId:int}")]
	[Authorize(Policy = AuthorizationPolicies.WriteCategories)]
	public async Task<DeleteResponseDto> DeleteCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
	{
		var success = await productGateway.DeleteCategory(categoryId, cancellationToken);

		if (!success)
		{
			return new DeleteResponseDto
			{
				Success = false,
				Message = $"Deletion failed. Category with ID {categoryId} not found"
			};
		}

		return new DeleteResponseDto
		{
			Success = true,
			Message = "Category deleted successfully"
		};
	}
}
