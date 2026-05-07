using MiniEcommerce.Basket.Application.Extensions;
using MiniEcommerce.Basket.Application.Interfaces;
using MiniEcommerce.Basket.Application.Services;
using MiniEcommerce.Basket.Infrastructure.Repositories;
using MiniEcommerce.Basket.Infrastructure.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration[DatabaseConstants.RedisConnectionStringKey];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDeveloperExceptionPage(); 
app.Run();
