using MiniEcommerce.Contracts.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.Product.API.Exceptions;

public sealed class CategoryNameEmptyException : AppException
{
    public CategoryNameEmptyException() : base("Category name cannot be empty.", HttpStatusCode.BadRequest){}
}
