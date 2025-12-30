using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId) : base($"Product with id '{productId}' was not found.") {}
}
