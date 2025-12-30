using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryNameEmptyException : Exception
{
    public CategoryNameEmptyException() : base("Category name cannot be empty."){}
}
