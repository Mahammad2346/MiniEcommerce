using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.DataAccessLayer.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MiniEcommerce.DataAccessLayer.Context;

public class MiniEcommerceDbContext : DbContext
{
	public MiniEcommerceDbContext(DbContextOptions<MiniEcommerceDbContext> options): base(options) { }
	public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
