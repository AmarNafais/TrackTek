using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];

ServiceConfigurator.ConfigureServices(builder.Services);

builder.Services.AddControllers();
builder.Services.ConfigureJwtAuthentication(jwtKey);
builder.Services.ConfigureSwaggerWithJwt();
builder.Services.AddScoped<IPasswordHasher<Data.Entities.User>, PasswordHasher<Data.Entities.User>>();
builder.Services.AddDbContext<Repository>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseJwtAuthenticationAndAuthorization();
app.MapControllers();

app.Run();
