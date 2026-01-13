using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryNotFoundException : AppException
{
    public CategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.", HttpStatusCode.NotFound) { } 
}
