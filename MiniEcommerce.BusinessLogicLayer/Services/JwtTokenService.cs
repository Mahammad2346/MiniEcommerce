using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
	public class JwtTokenService
	{
		private readonly string _secretKey;
		private readonly string _issuer;
		private readonly string _audience;

		public JwtTokenService(IConfiguration configuration)
		{
			_secretKey = configuration["Jwt:Key"]!;
			_issuer = configuration["Jwt:Issuer"]!;
			_audience = configuration["Jwt:Audience"]!;
		}

		public string GenerateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims, expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
