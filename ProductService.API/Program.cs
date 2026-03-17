using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.Product.API;
using MiniEcommerce.Product.API.Services.Grpc;
using MiniEcommerce.Product.BusinessLogicLayer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ProductGrpcService>();

app.Run();
