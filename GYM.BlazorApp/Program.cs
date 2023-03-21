using GYM.BlazorApp.Data;
using GYM.BlazorApp.Data.ViewModels;
using GYM.BlazorApp.Interfaces;
using GYM.BlazorApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IGenericService<CouchViewModel>, CouchesService>();
builder.Services.AddScoped<IGenericService<OrderViewModel>, OrdersService>();
builder.Services.AddScoped<IGenericService<VisitorViewModel>, VisitorsService>();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
        options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            options.Authority = "https://localhost:7181";
            options.ClientId = "mvc";
            options.ClientSecret = "secret";

            // When set to code, the middleware will use PKCE protection
            options.ResponseType = "code";

            // The "openid" and "profile" scopes are added automatically,
            // so we don't need to add them for this test
            //options.Scope.Add("openid");
            //options.Scope.Add("profile");

            // Save the tokens we receive from the IDP
            options.SaveTokens = true;

            // It's recommended to always get claims from the 
            // UserInfoEndpoint during the flow. 
            //options.GetClaimsFromUserInfoEndpoint = true;
        });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
