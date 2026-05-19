using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.User
{
    public sealed class UserAlreadyExistsException: AppException
    {
	public UserAlreadyExistsException(string email): base($"User with email '{email}' already exists.", HttpStatusCode.Conflict) {}

	}
}
