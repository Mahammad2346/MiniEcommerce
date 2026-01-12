using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Category;

public sealed class CategoryNameEmptyException : AppException
{
    public CategoryNameEmptyException() : base("Category name cannot be empty.", HttpStatusCode.BadRequest){}
}
