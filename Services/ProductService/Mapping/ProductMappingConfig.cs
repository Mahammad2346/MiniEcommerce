using Mapster;
using ProductEntity = MiniEcommerce.Product.DataAccessLayer.Entities.Product;
using GrpcProduct = MiniEcommerce.Product.Contracts.Protos.Product;
using System.Globalization;

namespace ProductService.Mapping;

public class ProductMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ProductEntity, GrpcProduct>().Map(dest => dest.Price, src => src.Price.ToString(CultureInfo.InvariantCulture));
	}
}
