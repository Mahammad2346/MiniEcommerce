using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Basket.Application.Dtos;
using MiniEcommerce.Basket.Application.Interfaces;

namespace MiniEcommerce.Basket.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BasketController(IBasketService basketService, IMapper mapper) : ControllerBase 
	{
		[HttpGet("{userName}")]
		public async Task<IActionResult> GetBasket(string userName)
		{
			var basket = await basketService.GetBasketByUserName(userName);
			return Ok(basket);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketDto basketDto)
		{
			var updateBasket = await basketService.UpdateBasket(basketDto);
			return Ok(updateBasket);
		}

		[HttpDelete("{userName}")]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			var result = await basketService.DeleteBasket(userName);
			return Ok(result);
		}
	}
}
