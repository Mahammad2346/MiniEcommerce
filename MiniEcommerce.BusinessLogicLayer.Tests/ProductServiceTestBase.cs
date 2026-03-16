using AutoFixture;
using AutoFixture.AutoNSubstitute;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.BusinessLogicLayer.Services;
using NSubstitute;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class ProductServiceTestBase
    {
        protected readonly IFixture Fixture;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IProductRepository ProductRepository;
		protected readonly ICategoryRepository CategoryRepository;

		protected ProductServiceTestBase()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                ConfigureMembers = true
            });

            UnitOfWork = Fixture.Create<IUnitOfWork>();
            ProductRepository = Fixture.Create<IProductRepository>();
			CategoryRepository = Fixture.Create<ICategoryRepository>();
			UnitOfWork.Categories.Returns(CategoryRepository);

			UnitOfWork.Products.Returns(ProductRepository);

		}

		protected ProductService CreateService()
		{
			return new ProductService(UnitOfWork);
		}
	}
}
