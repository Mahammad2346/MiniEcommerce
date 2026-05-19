using MiniEcommerce.Basket.Application.Dtos;
using MiniEcommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Interfaces
{
	public interface IBasketService
	{
		Task<ShoppingCartDto> GetBasketByUserName(string userName);
		Task<ShoppingCartDto> CreateBasket(CreateBasketDto basketDto);
		Task<ShoppingCartDto> UpdateBasket(UpdateBasketDto basketDto);
		Task<bool> DeleteBasket(string username);
	}
}
