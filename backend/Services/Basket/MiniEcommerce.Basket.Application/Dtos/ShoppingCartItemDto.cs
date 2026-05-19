using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Dtos;

public record ShoppingCartItemDto
(
	int ProductId,
	string ProductName,
	decimal ProductPrice,
	int Quantity
);
