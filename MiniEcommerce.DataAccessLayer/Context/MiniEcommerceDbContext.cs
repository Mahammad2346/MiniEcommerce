using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Configurations;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace MiniEcommerce.DataAccessLayer.Context
{
    public class MiniEcommerceDbContext : DbContext
    {
        public MiniEcommerceDbContext(DbContextOptions<MiniEcommerceDbContext>options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

        }
    }
}
