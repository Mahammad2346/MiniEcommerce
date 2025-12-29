using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product;

public sealed class ProductNameEmptyException : Exception
{
    public ProductNameEmptyException() : base("Product name cannot be empty."){}
}
