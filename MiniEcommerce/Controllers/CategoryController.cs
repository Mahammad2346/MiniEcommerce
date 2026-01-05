using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Responses;

namespace MiniEcommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var categories = await categoryService.GetAllCategoriesAsync(pageNumber, pageSize, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<CategoryDto>>.Ok(categories));
    }

    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetCategoryById(categoryId, cancellationToken);
        return Ok(ApiResponse<CategoryDto>.Ok(category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var createdCategory = await categoryService.CreateCategoryAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id }, ApiResponse<CategoryDto>.Ok(createdCategory));
    }

    [HttpPut("{categoryId:int}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var updatedCategory = await categoryService.UpdateCategoryAsync(categoryId, dto, cancellationToken);
        return Ok(ApiResponse<CategoryDto>.Ok(updatedCategory));
    }

    [HttpDelete("{categoryId:int}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
    {
        await categoryService.DeleteCategoryAsync(categoryId, cancellationToken);
        return NoContent();
    }
}
