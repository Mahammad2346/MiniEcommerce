using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.Product.API.Services;
using MiniEcommerce.Product.API.Services.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddScoped<ProductService>();

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