using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions;


public sealed class InvalidCategoryIdException : AppException
{
    public InvalidCategoryIdException(int categoryId) : base($"Category id '{categoryId}' must be greater than zero.", HttpStatusCode.BadRequest)
    {
    }
}
