using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Basket.Application.Exceptions;

public class BasketDeleteException : AppException
{
	public BasketDeleteException(string name)
		: base($"Basket for user '{name}' could not be deleted", HttpStatusCode.BadRequest)
	{
	}
}
