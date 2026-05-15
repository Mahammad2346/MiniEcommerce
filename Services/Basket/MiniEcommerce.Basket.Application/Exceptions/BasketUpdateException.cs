using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Basket.Application.Exceptions;

public class BasketUpdateException: AppException
{
	public BasketUpdateException(string name): base($"Basket for user '{name}' could not be updated.", HttpStatusCode.BadRequest)
	{
			
	}
}
