using Microsoft.AspNetCore.Identity;
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
        public async Task<User> RegisterAsync(string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(email, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
            var user = new User
            {
                Email = email,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow,
            };
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(email, cancellationToken);
            if (existingUser == null)
            {
                throw new Exception("User exception");
            }
            var result = passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid credentials");
            }
            return existingUser;
        }
    }
}
