using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Domain.Entities;

public class ShoppingCartItem
{
	public int ProductId { get; set; }
	public string ProductName { get; set; }
	public decimal ProductPrice { get; set; }
	public int Quantity { get; set; }
}
