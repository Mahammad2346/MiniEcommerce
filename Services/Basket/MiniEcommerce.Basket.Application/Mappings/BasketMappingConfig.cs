using Mapster;
using MiniEcommerce.Basket.Application.Dtos;
using MiniEcommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Mappings
{
	public class BasketMappingConfig: IRegister
	{
		public void Register (TypeAdapterConfig config)
		{
			config.NewConfig<ShoppingCart, ShoppingCartDto>();
			config.NewConfig<ShoppingCartItem, ShoppingCartItemDto>();

		}
	}
}
