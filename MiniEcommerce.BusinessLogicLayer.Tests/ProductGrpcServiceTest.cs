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
}
