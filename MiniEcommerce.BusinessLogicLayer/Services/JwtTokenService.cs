using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Services
{
	public class JwtTokenService: ITokenService
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

        public string Generate(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(60),
				Issuer = _issuer,
				Audience = _audience,
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);	
			return tokenHandler.WriteToken(token);
		}
	}
}
