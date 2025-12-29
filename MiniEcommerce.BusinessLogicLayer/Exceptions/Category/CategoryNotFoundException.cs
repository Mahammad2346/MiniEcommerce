using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(int categoryId): base($"Category with id '{categoryId}' was not found.") {}
}
