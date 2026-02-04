using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryAlreadyExistsException : AppException
{
    public CategoryAlreadyExistsException(string name) : base($"Category with name '{name}' already exists.", HttpStatusCode.Conflict) { }
}
