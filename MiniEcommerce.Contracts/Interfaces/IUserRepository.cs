using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
