using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions.Product;

public sealed class ProductNameEmptyException : AppException
{
    public ProductNameEmptyException() : base("Product name cannot be empty.", HttpStatusCode.BadRequest) {}
}
