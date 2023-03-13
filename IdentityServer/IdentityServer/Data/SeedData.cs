using IdentityModel;
using IdentityServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer.Data
{
    public static class SeedData
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();


            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }

        public static void EnsurePopulatedUsers(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                 .CreateScope().ServiceProvider.GetService<AuthApplicationContext>()!;

            if (!context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // Add Alice
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider
                .GetService<UserManager<ApplicationUser>>()!;

            var alice = userManager.FindByNameAsync("alice").Result;
            if (alice == null)
            {

                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(alice, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(alice, new Claim[]{
                                    new (JwtClaimTypes.Name, "Alice Smith"),
                                    new (JwtClaimTypes.GivenName, "Alice"),
                                    new (JwtClaimTypes.FamilyName, "Smith"),
                                    new (JwtClaimTypes.WebSite, "http://alice.com"),
                                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            // Add Bob
            var bob = userManager.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(bob, new Claim[]{
                    new (JwtClaimTypes.Name, "Bob Smith"),
                    new (JwtClaimTypes.GivenName, "Bob"),
                    new (JwtClaimTypes.FamilyName, "Smith"),
                    new (JwtClaimTypes.WebSite, "http://bob.com"),
                    new ("location", "somewhere")
                }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
