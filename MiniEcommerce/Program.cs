using Microsoft.Extensions.Options;
using MiniEcommerce.BusinessLogicLayer.Extensions;
using MiniEcommerce.Configurations;
using MiniEcommerce.ExceptionHandlingMiddleware;
using MiniEcommerce.Extensions;
using MiniEcommerce.Product.API;
using MiniEcommerce.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddAuth0Authentication(builder.Configuration);

builder.Services.Configure<ProductGrpcOptions>(
	builder.Configuration.GetSection("ProductGrpc"));

builder.Services.AddScoped<ProductGateway>();
builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>((sp, options) =>
{
	var config = sp.GetRequiredService<IOptions<ProductGrpcOptions>>().Value;
	options.Address = new Uri(config.Address);
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
