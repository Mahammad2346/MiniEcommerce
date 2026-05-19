using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.User
{
    public class InvalidCredentialsException: AppException
    {
		public InvalidCredentialsException(): base("Invalid email or password.", HttpStatusCode.Unauthorized)
		{
		}
	}
}
