using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Exceptions.Product;
public sealed class InvalidProductPriceException : AppException
{
    public InvalidProductPriceException(decimal price) : base($"Product price '{price}' must be greater than zero.", HttpStatusCode.BadRequest) {}
}
