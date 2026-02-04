using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(u => u.Id);

			builder.Property(u => u.Email)
				   .IsRequired()
				   .HasMaxLength(256);

			builder.HasIndex(u => u.Email)
				   .IsUnique();

			builder.Property(u => u.PasswordHash)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(u => u.Role)
				   .IsRequired();

			builder.Property(u => u.CreatedAt)
				   .IsRequired();
		}
	}
}
