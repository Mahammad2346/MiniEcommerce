using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Contracts.Dtos;
using MiniEcommerce.Product.API;

namespace MiniEcommerce.Services;

public class ProductGateway
{
	private readonly ProductGrpc.ProductGrpcClient grpcClient;

	public ProductGateway(ProductGrpc.ProductGrpcClient grpcClient)
	{
		this.grpcClient = grpcClient;
	}

	public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(int pageNumber, int pageSize, int? categoryId, CancellationToken cancellationToken)
	{
		var request = new GetProductsRequest
		{
			PageNumber = pageNumber,
			PageSize = pageSize,
			CategoryId = categoryId ?? 0
		};

		var response = await grpcClient.GetProductsAsync(request, cancellationToken: cancellationToken);

		var productDtos = response.Products.Select(p => new ProductDto(
			Id: p.Id,
			Name: p.Name,
			Price: (decimal)p.Price,
			Description: p.Description,
			CategoryId: p.CategoryId
		)).ToList();

		return productDtos;
	}

	public async Task<ProductDto> GetProductById(int productId, CancellationToken cancellationToken)
	{
		var request = new GetProductRequest
		{
			Id = productId,
		};

		var response = await grpcClient.GetProductAsync(request, cancellationToken: cancellationToken);

		var productDto = new ProductDto(
			Id: response.Product.Id,
			Name: response.Product.Name,
			Price: (decimal)response.Product.Price,
			Description: response.Product.Description,
			CategoryId: response.Product.CategoryId
		);

		return productDto;
	}


	public async Task<ProductDto> CreateProduct(CreateProductDto dto, CancellationToken cancellationToken)
	{
		var request = new CreateProductRequest
		{
			Name = dto.Name,
			Price = (double)dto.Price,
			CategoryId = dto.CategoryId,
			Description = dto.Description
		};

		var response = await grpcClient.CreateProductAsync(request, cancellationToken: cancellationToken);

		var productDto = new ProductDto(
			response.Product.Id,
			response.Product.Name,
			(decimal)response.Product.Price,
			response.Product.Description,
			response.Product.CategoryId
			);
		return productDto;
	}

	public async Task<ProductDto> UpdateProduct(int productId, UpdateProductDto dto, CancellationToken cancellationToken)
	{
		var request = new UpdateProductRequest
		{
			Id = productId,
			Name = dto.Name,
			Price = (double)dto.Price,
			Description = dto.Description,
			CategoryId = dto.CategoryId
		};

		var response = await grpcClient.UpdateProductAsync(request, cancellationToken: cancellationToken);

		var productDto = new ProductDto(response.Product.Id, response.Product.Name, (decimal)response.Product.Price, response.Product.Description,
		response.Product.CategoryId);

		return productDto;
	}

	public async Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken)
	{
		var request = new DeleteProductRequest
		{
			Id = productId
		};

		var response = await grpcClient.DeleteProductAsync(request, cancellationToken: cancellationToken);

		return response.Success;
	}
}