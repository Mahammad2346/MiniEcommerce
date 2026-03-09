using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions.Product;

public sealed class ProductCategoryNotFoundException : AppException
{
    public ProductCategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.", HttpStatusCode.BadRequest) { }
}
