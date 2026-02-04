using Microsoft.AspNetCore.Identity;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.BusinessLogicLayer.Exceptions.User;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Enums;
using MiniEcommerce.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        public async Task<User> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email);
			}
			var user = new User
            {
                Email = request.Email,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow,
            };
            var hashedPassword = passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;
            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser == null)
            {
				throw new InvalidCredentialsException();
			}
			var result = passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
				throw new InvalidCredentialsException();
			}
			return existingUser;
        }
    }
}
