using Microsoft.Extensions.Configuration;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class JwtTokenServiceTests
    {
        public void GenerateToken_ValidUser_ShouldReturnToken()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "ThisIsASecretKeyForTesting123!" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" }
            };

            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var service = new JwtTokenService(configuration);

            var user = new User
            {
                Id = 1,
                Email = "test@test.com",
                Role = UserRole.User
            };

            var token = service.GenerateToken(user);

			Assert.False(string.IsNullOrWhiteSpace(token));
		}
	}
}
