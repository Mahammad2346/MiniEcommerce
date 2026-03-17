using AutoFixture;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Testing;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.API.Services.Grpc;
using MiniEcommerce.Product.BusinessLogicLayer.Services;
using MiniEcommerce.Product.Contracts;
using MiniEcommerce.Product.Contracts.Protos;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Tests;

public class ProductGrpcServiceTest
{
	protected readonly IFixture Fixture;
	protected readonly IProductService productServiceMock;
	protected readonly ICategoryService categoryServiceMock;
	protected readonly ProductGrpcService sut;
	

	public ProductGrpcServiceTest()
	{
		Fixture = new Fixture();
		productServiceMock = Substitute.For<IProductService>();
		categoryServiceMock = Substitute.For<ICategoryService>();
		sut = new ProductGrpcService(productServiceMock, categoryServiceMock);
	}

	[Fact]
	public async Task GetProduct_WhenProductExists_ShouldReturnProductResponse()
	{
		var productId = 1;
		var fakeProduct = Fixture.Create<ProductDto>();

		productServiceMock.GetProduct(productId, Arg.Any<CancellationToken>()).Returns(fakeProduct);

		var request = new GetProductRequest
		{
			Id = productId
		};

		var mockContext = Substitute.For<Grpc.Core.ServerCallContext>();
		var result = await sut.GetProduct(request, mockContext);

		Assert.NotNull(result);
		Assert.NotNull(result.Product);
		Assert.Equal(fakeProduct.Id, result.Product.Id);
		Assert.Equal(fakeProduct.Name, result.Product.Name);

		await productServiceMock.Received(1).GetProduct(productId, Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task GetProduct_WhenProductDoesNotExist_ShouldReturnResponseWithNullProduct()
	{
		var productId = 1;
		productServiceMock.GetProduct(productId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(default(ProductDto)!));

		var request = new GetProductRequest
		{
			Id = productId
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.GetProduct(request, mockContext);

		Assert.NotNull(result);
		Assert.Null(result.Product);

		await productServiceMock.Received(1).GetProduct(productId, Arg.Any<CancellationToken>());
	}

	[Fact]

	public async Task GetProducts_ShouldReturnProducts()
	{
		var fakeProducts = Fixture.CreateMany<ProductDto>(3).ToList();

		productServiceMock.GetProductsAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int?>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult<IReadOnlyList<ProductDto>>(fakeProducts));

		var request = new GetProductsRequest
		{
			PageNumber = 1,
			PageSize = 10,
			CategoryId = 0
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.GetProducts(request, mockContext);

		Assert.NotNull(result);
		Assert.Equal(fakeProducts.Count, result.Products.Count);

		await productServiceMock.Received(1).GetProductsAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int?>(), Arg.Any<CancellationToken>());

	}

	[Fact]
	public async Task DeleteProduct_ShouldDeleteProduct()
	{
		var productId = 1;

		var request = new DeleteProductRequest
		{
			Id = productId
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.DeleteProduct(request, mockContext);

		Assert.NotNull(result);
		Assert.True(result.Success);

		await productServiceMock.Received(1).DeleteProductAsync(productId, Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task CreateProduct_ShouldReturnCreatedProduct()
	{
		var fakeProduct = Fixture.Create<ProductDto>();

		productServiceMock
			.CreateProductAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>())
			.Returns(fakeProduct);

		var request = new CreateProductRequest
		{
			Name = fakeProduct.Name,
			Price = (double)fakeProduct.Price,
			CategoryId = fakeProduct.CategoryId,
			Description = fakeProduct.Description
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.CreateProduct(request, mockContext);

		Assert.NotNull(result);
		Assert.NotNull(result.Product);
		Assert.Equal(fakeProduct.Name, result.Product.Name);

		await productServiceMock
			.Received(1)
			.CreateProductAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task UpdateProduct_ShouldReturnUpdatedProduct()
	{
		var fakeProduct = Fixture.Create<ProductDto>();

		productServiceMock.UpdateProductAsync(Arg.Any<int>(), Arg.Any<UpdateProductDto>(), Arg.Any<CancellationToken>()).Returns(fakeProduct);

		var request = new UpdateProductRequest
		{
			Id = fakeProduct.Id,
			Name = fakeProduct.Name,
			Price = (double)fakeProduct.Price,
			Description = fakeProduct.Description,
			CategoryId = fakeProduct.CategoryId
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.UpdateProduct(request, mockContext);

		Assert.NotNull(result);
		Assert.NotNull(result.Product);
		Assert.Equal(fakeProduct.Id, result.Product.Id);
		Assert.Equal(fakeProduct.Name, result.Product.Name);

		await productServiceMock.Received(1).UpdateProductAsync(Arg.Any<int>(), Arg.Any<UpdateProductDto>(), Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task CreateCategory_ShouldReturnCreatedCategory()
	{
		var fakeCategory = Fixture.Create<CategoryDto>();

		categoryServiceMock.CreateCategoryAsync(Arg.Any<CreateCategoryDto>(), Arg.Any<CancellationToken>()).Returns(fakeCategory);

		var request = new CreateCategoryRequest
		{
			Name = fakeCategory.Name
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.CreateCategory(request, mockContext);

		Assert.NotNull(result);
		Assert.NotNull(result.Category);
		Assert.Equal(fakeCategory.Name, result.Category.Name);

		await categoryServiceMock.Received(1).CreateCategoryAsync(Arg.Any<CreateCategoryDto>(), Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task GetCategory_WhenCategoryExists_ShouldReturnCategory()
	{
		var categoryId = 1;
		var fakeCategory = Fixture.Create<CategoryDto>();

		categoryServiceMock.GetCategoryByIdAsync(categoryId, Arg.Any<CancellationToken>()).Returns(fakeCategory);

		var request = new GetCategoryRequest
		{
			Id = categoryId
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.GetCategory(request, mockContext);

		Assert.NotNull(result);
		Assert.NotNull(result.Category);
		Assert.Equal(fakeCategory.Id, result.Category.Id);
		Assert.Equal(fakeCategory.Name, result.Category.Name);

		await categoryServiceMock.Received(1).GetCategoryByIdAsync(categoryId, Arg.Any<CancellationToken>());
	}
	[Fact]
	public async Task GetCategories_ShouldReturnCategories()
	{
		var fakeCategories = Fixture.CreateMany<CategoryDto>(3).ToList();

		categoryServiceMock.GetAllCategoriesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(fakeCategories);

		var request = new GetCategoriesRequest
		{
			PageNumber = 1,
			PageSize = 10
		};

		var mockContext = Substitute.For<ServerCallContext>();

		var result = await sut.GetCategories(request, mockContext);

		Assert.NotNull(result);
		Assert.Equal(fakeCategories.Count, result.Categories.Count);

		await categoryServiceMock.Received(1).GetAllCategoriesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>());
	}
}
