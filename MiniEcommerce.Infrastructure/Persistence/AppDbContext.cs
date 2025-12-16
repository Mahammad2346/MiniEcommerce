using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


    }
}
