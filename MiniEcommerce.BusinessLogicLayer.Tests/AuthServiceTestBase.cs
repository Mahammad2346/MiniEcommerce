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
    public abstract class AuthServiceTestBase
    {
        protected readonly IFixture Fixture;
        protected readonly UserManager<User> UserManager;
        protected readonly ITokenService TokenService;

        protected AuthServiceTestBase()
        {
            Fixture = new Fixture();

            Fixture.Customize(new AutoNSubstituteCustomization());
            var store = Fixture.Create<IUserStore<User>>();

			UserManager = Substitute.For<UserManager<User>>(store, null, null, null, null, null, null, null, null);

			TokenService = Fixture.Create<ITokenService>();
		}
		protected AuthService CreateService()
		{
			return new AuthService(UserManager, TokenService);
		}
	}
}
