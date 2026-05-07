using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Domain.Entities
{
	public class ShoppingCart
	{
		public string UserName { get; set; }
		public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
		public ShoppingCart(){

		}

		public ShoppingCart(string userName){
			UserName = userName;
		}

		public decimal TotalPrice { 
			get{
				return Items.Sum(item => item.ProductPrice * item.Quantity);
			}
		}
	}
}
