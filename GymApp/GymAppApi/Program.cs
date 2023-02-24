using GYM.API.Data;
using GYM.API.DI;
using GYM.API.Extensions;
using GYM.API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddCustomCors();
builder.Services.AddDependenciesApi(configuration);
builder.Services.AddControllers();
builder.Services.AddDbContext<GymDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Logger;

app.UseCors(CustomCors.DefaultCorsPolicy);

app.UseMiddleware<CustomExceptionHandler>(logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#pragma warning disable S1118
public partial class Program { }
#pragma warning restore S1118
