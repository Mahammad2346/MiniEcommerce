<<<<<<< feature/product-service-grpc
﻿using MiniEcommerce.Contracts.Exceptions.Common;
=======
﻿using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
>>>>>>> master
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
