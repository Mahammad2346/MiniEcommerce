using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Exceptions;

public sealed class CategoryNotFoundException : AppException
{
    public CategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.", HttpStatusCode.NotFound) { } 
}
