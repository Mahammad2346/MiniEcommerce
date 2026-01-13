using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product;

public sealed class ProductCategoryNotFoundException : AppException
{
    public ProductCategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.", HttpStatusCode.BadRequest) { }
}
