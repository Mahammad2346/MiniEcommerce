using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Identity;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class AuthServiceTestBase
    {
		protected readonly IFixture Fixture;
		protected readonly IUnitOfWork UnitOfWork;
		protected readonly IUserRepository UserRepository;
		protected readonly IPasswordHasher<User> PasswordHasher;

		public AuthServiceTestBase()
        {

			Fixture = new Fixture().Customize(new AutoNSubstituteCustomization
			{
				ConfigureMembers = true
			});

			UnitOfWork = Fixture.Create<IUnitOfWork>();
			UserRepository = Fixture.Create<IUserRepository>();
			PasswordHasher = Fixture.Create<IPasswordHasher<User>>();
		}

		protected AuthService CreateService()
		{
			return new AuthService(UnitOfWork, UserRepository, PasswordHasher);
		}
	}
}
