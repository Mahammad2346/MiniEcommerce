using AutoFixture;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using NSubstitute;
using System.Linq.Expressions;

namespace MiniEcommerce.BusinessLogicLayer.Tests;

public class CategoryServiceTests: CategoryServiceTestBase
{
    [Fact]
    public async Task CreateCategory_ValidName_ShouldCreateCategory()
    {
        var productRepository = Fixture.Create<IProductRepository>();
        UnitOfWork.Products.Returns(productRepository);

        CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);

        UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);
        var service = CreateService();

        var dto = Fixture.Build<CreateCategoryDto>().With(x => x.Name, "Electronics").Create();

        var result = await service.CreateCategoryAsync(dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Electronics", result.Name);

        CategoryRepository.Received(1).Add(Arg.Any<Category>());

        await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateCategory_DuplicateName_ShouldThrowException()
    {
        var productRepository = Fixture.Create<IProductRepository>();
        UnitOfWork.Products.Returns(productRepository);
		CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);

        var categoryService = CreateService();
        var dto = Fixture.Build<CreateCategoryDto>().With(x => x.Name, "Electronics").Create();

        await Assert.ThrowsAsync<CategoryAlreadyExistsException>(() => categoryService.CreateCategoryAsync(dto, CancellationToken.None));
	}

	[Fact]
    public async Task CreateCategory_EmptyName_ShouldThrowException()
    {
        var categoryService = CreateService();

        var dto = new CreateCategoryDto(" ");
        await Assert.ThrowsAsync<CategoryNameEmptyException>(() => categoryService.CreateCategoryAsync(dto, CancellationToken.None));
    }

    [Fact]
    public async Task GetCategoryById_CategoryNotFound_ShouldThrowException()
    {
        CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns((Category?)null);

        var categoryService = CreateService();

        await Assert.ThrowsAsync<CategoryNotFoundException>(() => categoryService.GetCategoryByIdAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteCategory_HasProducts_ShouldThrowException()
    {
        var productRepository = Fixture.Create<IProductRepository>();
        var category = Fixture.Create<Category>();
		UnitOfWork.Products.Returns(productRepository);
        productRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
        CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(category);
        var categoryService = CreateService();

        await Assert.ThrowsAsync<CategoryHasProductsException>(() => categoryService.DeleteCategoryAsync(1, CancellationToken.None));
	}

    [Fact]
    public async Task DeleteCategory_Valid_ShouldDeleteCategory()
    {

        var productRepository = Fixture.Create<IProductRepository>();
        var category = Fixture.Build<Category>().With(c => c.Name, "Electronics").Create();

        UnitOfWork.Products.Returns(productRepository);
        productRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
        CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(category);

        var categoryService = CreateService();

        var result = await categoryService.DeleteCategoryAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Electronics", result.Name);

        CategoryRepository.Received(1).Delete(category);
        await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
	}

    [Fact]
    public async Task UpdateCategory_DuplicateName_ShouldThrowException()
    {

        var existingCategory = Fixture.Build<Category>().With(c=>c.Name, "Electronics").Create();
        CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(existingCategory);
		CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
		var categoryService = CreateService();
        var dto = Fixture.Build<UpdateCategoryDto>().With(c => c.Name, "Electronics").Create();

        await Assert.ThrowsAsync<CategoryAlreadyExistsException>(() => categoryService.UpdateCategoryAsync(1, dto, CancellationToken.None));
	}

    [Fact]
    public async Task UpdateCategory_Valid_ShouldUpdateCategory()
    {
		var existingCategory = Fixture.Build<Category>().With(c => c.Name, "Electronics").Create();
		CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(existingCategory);
		CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
		var categoryService = CreateService();
		var dto = Fixture.Build<UpdateCategoryDto>().With(c => c.Name, "Books").Create();

        var result = await categoryService.UpdateCategoryAsync(1, dto, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal("Books", result.Name);
        await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
	}
}
