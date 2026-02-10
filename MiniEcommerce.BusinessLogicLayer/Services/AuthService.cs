using Azure.Core;
using Microsoft.AspNetCore.Identity;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.BusinessLogicLayer.Exceptions.User;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Enums;
using MiniEcommerce.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class AuthService(UserManager<User> userManager, ITokenService tokenService): IAuthService   
    {
        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
		
			var existingUser = await userManager.FindByEmailAsync(registerRequest.Email);
			if (existingUser != null)
			{
				throw new UserAlreadyExistsException(registerRequest.Email);
			}

			var user = new User
			{
				Email = registerRequest.Email,
				UserName = registerRequest.Email,
				Role = UserRole.User,
				CreatedAt = DateTime.UtcNow,
			};

			var result = await userManager.CreateAsync(user, registerRequest.Password);
			if (!result.Succeeded)
			{
				var firstError = result.Errors.FirstOrDefault()?.Description ?? "Register failed";
				throw new Exception(firstError);
			}
			return user;
		}
        public async Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
			var existingUser = await userManager.FindByEmailAsync(loginRequest.Email);
			if (existingUser == null)
			{
				throw new InvalidCredentialsException();
			}
			bool isPasswordValid = await userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

			if (!isPasswordValid)
			{
				throw new InvalidCredentialsException();
			}
			return tokenService.Generate(existingUser);
		}

     
    }
}
