
using Microsoft.EntityFrameworkCore;
using MiniEcommerce.DataAccessLayer.Extensions;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories;
using MiniEcommerce.Contracts.Interfaces;
using Mapster;
using MapsterMapper;
using MiniEcommerce.BusinessLogicLayer.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddMapping();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
