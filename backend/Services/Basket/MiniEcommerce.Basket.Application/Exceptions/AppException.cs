using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Basket.Application.Exceptions;

public class AppException: Exception
{
	public HttpStatusCode StatusCode { get; }
	public AppException(string message, HttpStatusCode statusCode) : base(message) {
		StatusCode = statusCode;
	}
}
