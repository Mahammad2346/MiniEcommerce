using MiniEcommerce.BusinessLogicLayer.Extensions;
using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.ExceptionHandlingMiddleware;
using MiniEcommerce.Extensions;
using MiniEcommerce.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddAuth0Authentication(builder.Configuration);

builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddGrpcReflection();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapOpenApi();
	app.MapGrpcReflectionService();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ProductGrpcService>(); 

await app.RunAsync();
