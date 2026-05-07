using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Dtos
{
	public record UpdateBasketDto(
	string UserName,
	List<ShoppingCartItemDto> Items
	);
}
