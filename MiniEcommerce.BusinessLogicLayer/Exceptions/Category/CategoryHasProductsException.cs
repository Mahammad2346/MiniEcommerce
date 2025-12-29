using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryHasProductsException : Exception
{
    public CategoryHasProductsException(int categoryId) : base($"Category '{categoryId}' cannot be deleted because it has products.") {}
}
