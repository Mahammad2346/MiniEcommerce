using AutoFixture;
using AutoFixture.AutoNSubstitute;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using NSubstitute;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class CategoryServiceTestBase
    {
        protected readonly IFixture Fixture;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IRepository<Category> CategoryRepository;

        protected CategoryServiceTestBase()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
				ConfigureMembers = true
            });

            UnitOfWork = Fixture.Create<IUnitOfWork>();
            CategoryRepository = Fixture.Create<IRepository<Category>>();

            UnitOfWork.Categories.Returns(CategoryRepository);
        }

        protected CategoryService CreateService()
        {
            return new CategoryService(UnitOfWork);
        }
    }
}
