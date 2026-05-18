using MiniEcommerce.Basket.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace MiniEcommerce.Basket.API.Middlewares;

public class BasketExceptionMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		if (exception is AppException appException)
		{
			context.Response.StatusCode = (int)appException.StatusCode;

			var response = new
			{
				error = appException.Message
			};

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}

		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var fallbackResponse = new
		{
			error = "An unexpected error occurred in Basket Service"
		};

		return context.Response.WriteAsync(JsonSerializer.Serialize(fallbackResponse));
	}
}
