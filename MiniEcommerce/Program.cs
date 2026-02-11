using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniEcommerce.BusinessLogicLayer.Extensions;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.ExceptionHandlingMiddleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.Authority = "https://dev-ax8s6bs03qdw4zy0.us.auth0.com/";
		options.Audience = "https://miniecommerce-api";
	});

builder.Services.AddAuthorization();
builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services
	.AddIdentityCore<User>()
	.AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<MiniEcommerceDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
