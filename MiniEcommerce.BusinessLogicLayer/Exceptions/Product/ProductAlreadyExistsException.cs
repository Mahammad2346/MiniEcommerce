using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product
{
    public sealed class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string name, int categoryId) : base($"Product '{name}' already exists in category '{categoryId}'.") {}
    }
}
