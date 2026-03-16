using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.DataAccessLayer.Configurations;
using ProductEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Product;
using CategoryEntity = MiniEcommerce.Product.BusinessLogicLayer.Entities.Category;
namespace MiniEcommerce.Product.DataAccessLayer;


public class MiniEcommerceDbContext : DbContext
{
	public MiniEcommerceDbContext(DbContextOptions<MiniEcommerceDbContext> options): base(options) { }
	public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
