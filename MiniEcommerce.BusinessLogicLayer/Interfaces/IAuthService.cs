using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest registerRequest);
        Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
