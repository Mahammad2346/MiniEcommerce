using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.DataAccessLayer.Entities;
using MiniEcommerce.Product.DataAccessLayer.Configurations;
using ProductEntity = MiniEcommerce.Product.DataAccessLayer.Entities.Product;
namespace MiniEcommerce.Product.DataAccessLayer;

public class MiniEcommerceDbContext : DbContext
{
	public MiniEcommerceDbContext(DbContextOptions<MiniEcommerceDbContext> options): base(options) { }
	public DbSet<ProductEntity> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
