using Microsoft.EntityFrameworkCore;
using VehicleParts.Infrastructure.Persistance;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<ISalesService, SalesService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
