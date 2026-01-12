using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Common;

public class AppException: Exception
{
    public HttpStatusCode StatusCode { get; set; }
    protected AppException(string message, HttpStatusCode statusCode): base(message)
    {
        StatusCode = statusCode; 
    }
}
