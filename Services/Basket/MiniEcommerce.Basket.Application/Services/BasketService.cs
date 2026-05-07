using Mapster;
using MiniEcommerce.Basket.Application.Dtos;
using MiniEcommerce.Basket.Application.Interfaces;
using MiniEcommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Basket.Application.Services
{
	public class BasketService(IBasketRepository repository) : IBasketService
	{
		private async Task<ShoppingCartDto> InternalUpdateOrUpdateBasket(string userName, List<ShoppingCartItem> newItems)
		{
			var existingCart = await repository.GetBasketByUserName(userName);
			var cart = existingCart ?? new ShoppingCart { UserName = userName };

			foreach (var item in newItems)
			{
				var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == item.ProductId);

				if (existingItem != null)
				{
					existingItem.Quantity += item.Quantity;
				}
				else
				{
					cart.Items.Add(item);
				}
			}

			var updatedCart = await repository.UpdateCart(cart);

			if (updatedCart == null)
				throw new Exception("Basket could not be updated");

			return updatedCart.Adapt<ShoppingCartDto>();
		}

		public async Task<ShoppingCartDto> CreateBasket(CreateBasketDto basketDto)
		{
			var items = basketDto.Items.Adapt<List<ShoppingCartItem>>();
			return await InternalUpdateOrUpdateBasket(basketDto.UserName, items);
		}

		public async Task<ShoppingCartDto> UpdateBasket(UpdateBasketDto basketDto)
		{
			var items = basketDto.Items.Adapt<List<ShoppingCartItem>>();
			return await InternalUpdateOrUpdateBasket(basketDto.UserName, items);
		}

		public async Task<bool> DeleteBasket(string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new Exception("Not found");
			else
				await repository.DeleteBasket(username);

			return true;

		}

		public async Task<ShoppingCartDto> GetBasketByUserName(string userName)
		{
			var cart = await repository.GetBasketByUserName(userName);
			if (cart is null)
			{
				cart = new ShoppingCart(userName);
			}

			var shoppingCart = cart.Adapt<ShoppingCartDto>();

			return shoppingCart;
		}
	}
}
