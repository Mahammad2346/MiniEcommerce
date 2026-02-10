using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Exceptions.User;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using Moq;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
namespace MiniEcommerce.BusinessLogicLayer.Tests
{
	public class AuthServiceTest: AuthServiceTestBase
	{

		[Fact]
		public async Task Register_Valid_ShouldCreateUser()
		{
			var user = Fixture.Create<RegisterRequest>();
			UserManager.FindByEmailAsync(user.Email).Returns(Task.FromResult<User?>(null));

			UserManager.CreateAsync(Arg.Any<User>(), user.Password).Returns(Task.FromResult(IdentityResult.Success));

			var service = CreateService();

			var result = await service.RegisterAsync(user);

			Assert.NotNull(result);
			Assert.Equal(user.Email, result.Email);
			await UserManager.Received(1).CreateAsync(Arg.Any<User>(), user.Password);

		}

		[Fact]
		public async Task Login_InvalidPassword_ShouldThrowException()
		{
			var request = Fixture.Build<LoginRequest>().With(m => m.Email, "test@gmail.com").With(p => p.Password, "Test123!").Create();
			var existingUser = Fixture.Build<User>().With(u => u.Email, request.Email).Create();
			UserManager.FindByEmailAsync(request.Email).Returns(existingUser);

			UserManager.CheckPasswordAsync(existingUser, request.Password).Returns(false);

			var service = CreateService();

			await Assert.ThrowsAsync<InvalidCredentialsException>(() => service.LoginAsync(request, CancellationToken.None));
		}

		[Fact]

		public async Task Login_Valid()
		{
			var request = Fixture.Build<LoginRequest>().With(m => m.Email, "test@gmail.com").With(p => p.Password, "Test123!").Create();
			var existingUser = Fixture.Build<User>().With(u => u.Email, request.Email).Create();

			UserManager.FindByEmailAsync(request.Email).Returns(existingUser);
			UserManager.CheckPasswordAsync(existingUser, request.Password).Returns(true);

			var service = CreateService();

			TokenService.Generate(existingUser).Returns("fake-jwt-token");

			var result = await service.LoginAsync(request, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal("fake-jwt-token", result);

			TokenService.Received(1).Generate(existingUser);
		}
	}
}
