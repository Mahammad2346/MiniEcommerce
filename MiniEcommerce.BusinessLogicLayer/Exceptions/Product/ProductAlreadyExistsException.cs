using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product;

public sealed class ProductAlreadyExistsException : AppException
{
    public ProductAlreadyExistsException(string name, int categoryId) : base($"Product '{name}' already exists in category '{categoryId}'.", HttpStatusCode.Conflict) {}
}
