using MiniEcommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Interfaces;

public interface IBasketRepository
{
	Task<ShoppingCart?> GetBasketByUserName(string userName);
	Task<ShoppingCart?> UpdateCart(ShoppingCart basket);
	Task<bool> DeleteBasket(string userName);
}
