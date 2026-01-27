using AutoFixture;
using AutoFixture.AutoNSubstitute;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class ProductServiceTestBase
    {
        protected readonly IFixture Fixture;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IProductRepository ProductRepository;
		protected readonly IRepository<Category> CategoryRepository;

		protected ProductServiceTestBase()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                ConfigureMembers = true
            });

            UnitOfWork = Fixture.Create<IUnitOfWork>();
            ProductRepository = Fixture.Create<IProductRepository>();
			CategoryRepository = Fixture.Create<IRepository<Category>>();
			UnitOfWork.Categories.Returns(CategoryRepository);

			UnitOfWork.Products.Returns(ProductRepository);

		}

		protected ProductService CreateService()
		{
			return new ProductService(UnitOfWork);
		}
	}
}
