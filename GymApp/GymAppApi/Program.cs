using GYM.API.Const.Authorization;
using GYM.API.Data;
using GYM.API.DI;
using GYM.API.Extensions;
using GYM.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddCustomCors();

builder.Services.AddDependenciesApi(configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Swagger API v1",
        Title = "Swagger with IdentityServer4",
        Version = "1.0.0"
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:7181/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"SwaggerAPI", "Swagger API DEMO"}
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7181";
    options.RequireHttpsMetadata = false;
    options.Audience = "SwaggerAPI";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyAuthorizationParameters.PolicyDefaultScheme,
        policy => policy.RequireScope(
            PolicyAuthorizationParameters.ScopeGymApi,
            PolicyAuthorizationParameters.ScopeNewsApi,
            PolicyAuthorizationParameters.ScopeSwaggerApi
            ));
});


builder.Services.AddDbContext<GymDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("GymDbDefault")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

var app = builder.Build();

var logger = app.Logger;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Demo");
        options.DocumentTitle = "Title";
        options.DocExpansion(DocExpansion.List);
        options.OAuthClientId("swagger_id");
        options.OAuthScopeSeparator(" ");
        options.OAuthClientSecret("secret");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

app.UseRouting();

app.UseCors(CustomCors.DefaultCorsPolicy);

app.UseMiddleware<CustomExceptionHandler>(logger);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();

app.Run();

#pragma warning disable S1118
public partial class Program { }
#pragma warning restore S1118
