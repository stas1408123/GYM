using AutoFixture;
using GYM.DAL.EF;
using GYM.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GYM.API.IntegrationTests
{
    public class GymWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<GymAppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<GymAppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryGymAppTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<GymAppDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        InitializeDb(appContext);
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred creating the DB in Memory.");

                    }
                }
            });
        }

        private void InitializeDb(GymAppDbContext dbContext)
        {
            var fixture = new Fixture();

            //add couches
            IEnumerable<CouchEntity> couches = fixture.Build<CouchEntity>().Without(p => p.Id).Without(p => p.Visitors).CreateMany(5).ToList();

            dbContext.CouchEntities.AddRange(couches);
            dbContext.SaveChanges();

            // add visitors
            var visitors = fixture.Build<VisitorEntity>()
                .Without(p => p.Id)
                .With(p => p.Orders, new List<OrderEntity>())
                .With(p => p.Couches, new List<CouchEntity>())
                .CreateMany(5);

            dbContext.VisitorEntities.AddRange(visitors);
            dbContext.SaveChanges();

            IEnumerable<OrderEntity> ordersEntities = fixture.Build<OrderEntity>()
                .Without(p => p.Id)
                .Without(p => p.Visitor)
                //.With(p => p.VisitorId, 1)
                .CreateMany(5).ToList();

            dbContext.OrderEntities.AddRange(ordersEntities);
            dbContext.SaveChanges();
        }

    }
}
