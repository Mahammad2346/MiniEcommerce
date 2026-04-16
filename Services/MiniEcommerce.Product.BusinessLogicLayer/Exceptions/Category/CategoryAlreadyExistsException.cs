using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.BusinessLogicLayer.Exceptions;
public sealed class CategoryAlreadyExistsException : AppException
{
    public CategoryAlreadyExistsException(string name) : base($"Category with name '{name}' already exists.", HttpStatusCode.Conflict) { }
}
