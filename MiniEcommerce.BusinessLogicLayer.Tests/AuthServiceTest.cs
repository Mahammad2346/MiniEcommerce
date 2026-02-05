using AutoFixture;
using Microsoft.AspNetCore.Identity;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.BusinessLogicLayer.Exceptions.User;
using MiniEcommerce.Contracts.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text;
namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class AuthServiceTest: AuthServiceTestBase
    {
        [Fact]
		public async Task Register_Valid_ShouldCreateUser()
        {
			UserRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(c => Task.FromResult<User?>(null));
			PasswordHasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("hashed-password");
			UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

			var service = CreateService();

			var dto = Fixture.Build<RegisterRequest>().With(x => x.Email, "test@gmail.com").With(x => x.Password, "12345").Create();	
			var result = await service.RegisterAsync(dto, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(dto.Email, result.Email);

			UserRepository.Received(1).Add(Arg.Any<User>());
			await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
		}

		[Fact]
		public async Task Login_UserNotFound_ShouldThrowException()
		{
			UserRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(c=> Task.FromResult<User?>(null));

			var service = CreateService();

			var request = Fixture.Build<LoginRequest>().With(x => x.Email, "notfound@test.com").With(x => x.Password, "123456").Create();

			await Assert.ThrowsAsync<InvalidCredentialsException>(() => service.LoginAsync(request, CancellationToken.None));
		}

		[Fact]
		public async Task Login_WrongPassword_ShouldThrowException()
		{
			var user = Fixture.Create<User>();

			UserRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(x => Task.FromResult<User?>(user));
			PasswordHasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Failed);

			var service = CreateService();

			var request = Fixture.Build<LoginRequest>().With(x => x.Email, "test@test.com").With(x => x.Password, "wrong-password").Create();

			await Assert.ThrowsAsync<InvalidCredentialsException>(() =>service.LoginAsync(request, CancellationToken.None));
		}

		[Fact]
		public async Task Login_Valid_ShouldReturnUser()
		{
			var user = Fixture.Create<User>();

			UserRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(x => Task.FromResult<User?>(user));
			PasswordHasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);

			var service = CreateService();

			var request = Fixture.Build<LoginRequest>().With(x => x.Email, "test@test.com").With(x => x.Password, "correct-password").Create();

			var result = await service.LoginAsync(request, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(user.Id, result.Id);
			Assert.Equal(user.Email, result.Email);
		}
	}
}
