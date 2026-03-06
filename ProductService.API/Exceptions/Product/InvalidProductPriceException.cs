using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions.Product;

public sealed class InvalidProductPriceException : AppException
{
    public InvalidProductPriceException(decimal price) : base($"Product price '{price}' must be greater than zero.", HttpStatusCode.BadRequest) {}
}
