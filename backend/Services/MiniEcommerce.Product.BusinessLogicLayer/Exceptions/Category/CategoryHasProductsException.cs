using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Exceptions;
public sealed class CategoryHasProductsException : AppException
{
    public CategoryHasProductsException(int categoryId) : base($"Category '{categoryId}' cannot be deleted because it has products.", HttpStatusCode.UnprocessableEntity) {}
}
