using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions.Product;

public sealed class ProductNotFoundException : AppException
{
    public ProductNotFoundException(int productId) : base($"Product with id '{productId}' was not found.", HttpStatusCode.NotFound) {}
}
