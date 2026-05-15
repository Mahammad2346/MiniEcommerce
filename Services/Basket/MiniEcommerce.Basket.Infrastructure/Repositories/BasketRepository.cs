using Microsoft.Extensions.Caching.Distributed;
using MiniEcommerce.Basket.Application.Interfaces;
using MiniEcommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MiniEcommerce.Basket.Infrastructure.Repositories;

public class BasketRepository(IDistributedCache cache) : IBasketRepository
{
	public async Task<bool> DeleteBasket(string userName)
	{
		await cache.RemoveAsync(userName);
		return true;
	}
	public async Task<ShoppingCart?> GetBasketByUserName(string userName)
	{
		var basketJson = await cache.GetStringAsync(userName);
		if(string.IsNullOrEmpty(basketJson)){
			return null;
		}

		return JsonSerializer.Deserialize<ShoppingCart>(basketJson)!;
	}
	public async Task<ShoppingCart?> UpdateCart(ShoppingCart basket)
	{
		var basketJson = JsonSerializer.Serialize(basket);
		await cache.SetStringAsync(basket.UserName, basketJson);
		return await GetBasketByUserName(basket.UserName);
	}
}
