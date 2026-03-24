using Grpc.Core;
using Mapster;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using GrpcCategory = MiniEcommerce.Contracts.Protos.Category;
using GrpcProduct = MiniEcommerce.Contracts.Protos.Product;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Protos;

namespace MiniEcommerce.GrpcServices;

public class ProductGrpcService(
	IProductService productService,
	ICategoryService categoryService
) : ProductGrpc.ProductGrpcBase
{
	public override async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request, ServerCallContext context)
	{
		var category = await categoryService.CreateCategoryAsync(
			new CreateCategoryDto(request.Name),
			context.CancellationToken
		);

		return new CreateCategoryResponse
		{
			Category = category.Adapt<GrpcCategory>()
		};
	}

	public override async Task<GetCategoryResponse> GetCategory(GetCategoryRequest request, ServerCallContext context)
	{
		var category = await categoryService.GetCategoryByIdAsync(request.Id, context.CancellationToken);

		return new GetCategoryResponse
		{
			Category = category.Adapt<GrpcCategory>()
		};
	}

	public override async Task<GetCategoriesResponse> GetCategories(GetCategoriesRequest request, ServerCallContext context)
	{
		var categories = await categoryService.GetAllCategoriesAsync(
			request.PageNumber,
			request.PageSize,
			context.CancellationToken
		);

		var response = new GetCategoriesResponse();
		response.Categories.AddRange(categories.Adapt<IEnumerable<GrpcCategory>>());

		return response;
	}

	public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
	{
		var product = await productService.GetProduct(request.Id, context.CancellationToken);

		return new GetProductResponse
		{
			Product = product.Adapt<GrpcProduct>()
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
		response.Products.AddRange(products.Adapt<IEnumerable<GrpcProduct>>());

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
			Product = product.Adapt<GrpcProduct>()
		};
	}

	public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
	{
		var product = await productService.UpdateProductAsync(
			request.Id,
			new UpdateProductDto(
				request.Name,
				(decimal)request.Price,
				request.Description,
				request.CategoryId
			),
			context.CancellationToken
		);

		return new UpdateProductResponse
		{
			Product = product.Adapt<GrpcProduct>()
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