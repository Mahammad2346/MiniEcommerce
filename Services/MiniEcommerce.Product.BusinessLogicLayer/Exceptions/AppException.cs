using System.Net;

namespace MiniEcommerce.Product.API.Exceptions;

public class AppException : Exception
{
	public HttpStatusCode StatusCode { get; }

	protected AppException(string message, HttpStatusCode statusCode) : base(message)
	{
		StatusCode = statusCode;
	}
}
