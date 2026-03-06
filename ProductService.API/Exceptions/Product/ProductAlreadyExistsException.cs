using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions.Product;

public sealed class ProductAlreadyExistsException : AppException
{
    public ProductAlreadyExistsException(string name, int categoryId) : base($"Product '{name}' already exists in category '{categoryId}'.", HttpStatusCode.Conflict) {}
}
