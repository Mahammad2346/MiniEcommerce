using ProductService.Mapping;
using MiniEcommerce.Product.BusinessLogicLayer.Extensions;
using MiniEcommerce.Product.DataAccessLayer.Extensions;
using MiniEcommerce.Product.Grpc.Services.Grpc;
using ProductService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddMapping();
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

await app.RunAsync();
