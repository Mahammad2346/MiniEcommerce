using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Dtos;

public record ShoppingCartDto
(
	string UserName,
	List<ShoppingCartItemDto> Items,
	decimal TotalPrice
);
