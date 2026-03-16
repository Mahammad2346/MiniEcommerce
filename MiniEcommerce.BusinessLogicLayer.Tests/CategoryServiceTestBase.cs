using AutoFixture;
using AutoFixture.AutoNSubstitute;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.BusinessLogicLayer.Services;
using NSubstitute;

namespace MiniEcommerce.Product.BusinessLogicLayer.Tests
{
	public class CategoryServiceTestBase
	{
		protected readonly IFixture Fixture;
		protected readonly IUnitOfWork UnitOfWork;
		protected readonly ICategoryRepository CategoryRepository;

		protected CategoryServiceTestBase()
		{
			Fixture = new Fixture().Customize(new AutoNSubstituteCustomization
			{
				ConfigureMembers = true
			});

			UnitOfWork = Fixture.Create<IUnitOfWork>();
			CategoryRepository = Fixture.Create<ICategoryRepository>();

			UnitOfWork.Categories.Returns(CategoryRepository);
		}

		protected CategoryService CreateService()
		{
			return new CategoryService(UnitOfWork);
		}
	}
}
