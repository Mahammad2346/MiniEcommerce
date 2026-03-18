using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.API;
using MiniEcommerce.Product.BusinessLogicLayer.Entities;
using MiniEcommerce.Product.DataAccessLayer;
using MiniEcommerce.Product.DataAccessLayer.Repositories;

namespace MiniEcommerce.BusinessLogicLayer.Tests;

public class RepositoryIntegrationTest
{
	private MiniEcommerceDbContext CreateContext()
	{
		var options = new DbContextOptionsBuilder<MiniEcommerceDbContext>().UseInMemoryDatabase("TestDb").Options;

		return new MiniEcommerceDbContext(options);
	}

	[Fact]
	public async Task Repository_Should_Work()
	{
		var context = CreateContext();

		var productRepo = new ProductRepository(context);
		var categoryRepo = new CategoryRepository(context);

		var unitOfWork = new UnitOfWork(context, productRepo, categoryRepo);

		var category = new Category
		{
			Name = "TestCategory"
		};

		unitOfWork.Categories.Add(category);
		await unitOfWork.SaveChangesAsync(CancellationToken.None);

		var exists = await unitOfWork.Categories.AnyAsync(c => c.Name == "TestCategory", CancellationToken.None);

		Assert.True(exists);
	}
}