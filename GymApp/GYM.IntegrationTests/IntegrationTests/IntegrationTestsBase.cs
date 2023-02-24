using AutoFixture;
using GYM.DAL.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class IntegrationTestsBase
    {
        public IntegrationTestsBase()
        {
            FixtureForTests = new Fixture();

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<GymAppDbContext>));

                    services.Remove(dbContextDescriptor!);

                    services.AddDbContext<GymAppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryGymAppTest");
                    });
                });
            });

            ClientForTests = webHost.CreateClient();
            DbContextForTests = webHost.Services.CreateScope().ServiceProvider.GetService<GymAppDbContext>()!;
        }

        public HttpClient ClientForTests { get; }

        public GymAppDbContext DbContextForTests { get; }

        public Fixture FixtureForTests { get; }
    }
}
