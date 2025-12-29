using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category
{

    public sealed class InvalidCategoryIdException : Exception
    {
        public InvalidCategoryIdException(int categoryId) : base($"Category id '{categoryId}' must be greater than zero.")
        {
        }
    }

}
