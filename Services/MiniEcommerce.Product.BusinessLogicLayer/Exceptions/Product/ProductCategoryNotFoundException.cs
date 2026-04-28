using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Exceptions.Product;
public sealed class ProductCategoryNotFoundException : AppException
{
    public ProductCategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.", HttpStatusCode.BadRequest) { }
}
