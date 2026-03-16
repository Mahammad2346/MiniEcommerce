using Mapster;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;
using GrpcProduct = MiniEcommerce.Product.Contracts.Protos.Product;

namespace MiniEcommerce.Product.API.Mapping;

public class ProductMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ProductEntity, GrpcProduct>()
			  .Map(dest => dest.Price, src => (double)src.Price);
	}
}