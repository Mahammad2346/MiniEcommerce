using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
	{
        public UserRepository(MiniEcommerceDbContext dbcontext): base(dbcontext){}
       
        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return DbSet.FirstOrDefaultAsync(e=>e.Email == email, cancellationToken);
            
        }
    }
}
