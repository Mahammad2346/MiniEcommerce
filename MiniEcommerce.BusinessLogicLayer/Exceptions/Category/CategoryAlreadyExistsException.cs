using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException(string name) : base($"Category with name '{name}' already exists."){}
}
