using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Product
{
    public sealed class InvalidProductPriceException : Exception
    {
        public InvalidProductPriceException(decimal price) : base($"Product price '{price}' must be greater than zero.") {}
    }
}
