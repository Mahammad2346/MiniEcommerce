using Grpc.Core;
using MiniEcommerce.Contracts.Dtos;
using MiniEcommerce.Contracts.Grpc;
namespace MiniEcommerce.Product.API.Services.Grpc;
using GrpcProduct = MiniEcommerce.Contracts.Grpc.Product;
public class ProductGrpcService(ProductService productService) : ProductGrpc.ProductGrpcBase
{
	public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
	{
		var product = await productService.GetProduct(request.Id, context.CancellationToken);

		return new GetProductResponse
		{
			Product = new GrpcProduct
			{
				Id = product.Id,
				Name = product.Name,
				Price = (double)product.Price,
				Description = product.Description,
				CategoryId = product.CategoryId,
			}
		};
	}
	public override async Task<GetProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
	{
		var products = await productService.GetProductsAsync(
			request.PageNumber,
			request.PageSize,
			request.CategoryId == 0 ? null : request.CategoryId,
			context.CancellationToken
		);

		var response = new GetProductsResponse();

		response.Products.AddRange(products.Select(p => new GrpcProduct
		{
			Id = p.Id,
			Name = p.Name,
			Price = (double)p.Price,
			Description = p.Description,
			CategoryId = p.CategoryId
		}));

		return response;
	}
	public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
	{
		var product = await productService.CreateProductAsync(
			new CreateProductDto(
				request.Name,
				(decimal)request.Price,
				request.CategoryId,
				request.Description
			),
			context.CancellationToken
		);

		return new CreateProductResponse
		{
			Product = new GrpcProduct
			{
				Id = product.Id,
				Name = product.Name,
				Price = (double)product.Price,
				CategoryId = product.CategoryId,
				Description = product.Description
			}
		};
	}

	public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request,ServerCallContext context)
	{
		var product = await productService.UpdateProductAsync(
			request.Id,
			new UpdateProductDto(
				Name: request.Name,
				Price: (decimal)request.Price,
				Description: request.Description,
				CategoryId: request.CategoryId
			),
			context.CancellationToken
		);

		return new UpdateProductResponse
		{
			Product = new GrpcProduct
			{
				Id = product.Id,
				Name = product.Name,
				Price = (double)product.Price,
				Description = product.Description,
				CategoryId = product.CategoryId
			}
		};
	}

	public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
	{
		await productService.DeleteProductAsync(request.Id, context.CancellationToken);

		return new DeleteProductResponse
		{
			Success = true
		};
	}
}

