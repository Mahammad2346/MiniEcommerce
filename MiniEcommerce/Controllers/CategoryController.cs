using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Interfaces;

namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "read:categories")]
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await categoryService.GetAllCategoriesAsync(pageNumber, pageSize, cancellationToken);
    }

    [HttpGet("{categoryId:int}")]
	[Authorize(Policy = "read:categories")]
	public async Task<CategoryDto> GetCategoryById([FromRoute] int categoryId, CancellationToken cancellationToken)
    {
        return await categoryService.GetCategoryByIdAsync(categoryId, cancellationToken);
    }

    [HttpPost]
	[Authorize(Policy = "write:categories")]
	public async Task<CategoryDto> CreateCategory([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        return await categoryService.CreateCategoryAsync(dto, cancellationToken);
    }

    [HttpPut("{categoryId:int}")]
	[Authorize(Policy = "write:categories")]
	public async Task<CategoryDto> UpdateCategory([FromRoute] int categoryId, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
       return await categoryService.UpdateCategoryAsync(categoryId, dto, cancellationToken);
    }

	[HttpDelete("{categoryId:int}")]
	[Authorize(Policy = "write:categories")]

	public async Task<CategoryDto> DeleteCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
    {
        return await categoryService.DeleteCategoryAsync(categoryId, cancellationToken);
    }
}
