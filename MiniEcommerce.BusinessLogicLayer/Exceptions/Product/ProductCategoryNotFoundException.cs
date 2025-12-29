using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product
{
    public sealed class ProductCategoryNotFoundException : Exception
    {
        public ProductCategoryNotFoundException(int categoryId) : base($"Category with id '{categoryId}' was not found.") { }
    }
}
