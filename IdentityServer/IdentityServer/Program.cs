using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//Identity with Db

builder.Services.AddDbContext<AuthApplicationContext>(options =>
     options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")!))
    .AddIdentity<ApplicationUser, ApplicationRole>(config =>
    {
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AuthApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "IdentityServer.Cookies";
});

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddIdentityServer()
.AddAspNetIdentity<ApplicationUser>()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
            sql => sql.MigrationsAssembly(migrationsAssembly));
})
.AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData.InitializeDatabase(app);

SeedData.EnsurePopulatedUsers(app);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
