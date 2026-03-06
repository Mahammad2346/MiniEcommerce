<<<<<<< feature/product-service-grpc
=======
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
>>>>>>> master
using MiniEcommerce.BusinessLogicLayer.Extensions;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.ExceptionHandlingMiddleware;
using MiniEcommerce.Extensions;
<<<<<<< feature/product-service-grpc
using MiniEcommerce.Product.API;
using MiniEcommerce.Services;
=======
using System.Text;
>>>>>>> master

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
<<<<<<< feature/product-service-grpc
=======

builder.Services.AddAuth0Authentication(builder.Configuration);

builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);
>>>>>>> master

builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddAuth0Authentication(builder.Configuration);
builder.Services.AddScoped<ProductGateway>();
builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(options =>
{
	options.Address = new Uri("https://localhost:7008");
});
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
