using AutoFixture;
using GYM.API.Models;
using GYM.DAL.EF;
using GYM.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class CouchesControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly GymAppDbContext _dbContext;
        private readonly Fixture _fixture;
        private const string RouteWithoutId = "api/Couches";
        private const string RouteWithId = "api/Couches/";

        public CouchesControllerIntegrationTests()
        {
            _fixture = new Fixture();

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

            _client = webHost.CreateClient();
            _dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<GymAppDbContext>()!;
            _dbContext.CouchEntities.AddRange(GetCouchesEntityForTest());
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetCouches_HasNotData_ReturnsStatusOkAndAllCouches()
        {
            //Arrange
            var couch = _dbContext.CouchEntities.LastOrDefault();

            //Act
            var response = await _client.GetAsync(RouteWithoutId);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(couch!.FirstName);
        }

        [Fact]
        public async Task GetCouch_InputValidId_ReturnsStatusOkAndCouch()
        {
            //Arrange
            var couch = _dbContext.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + couch.Id;

            //Act
            var response = await _client.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(couch.FirstName);
        }

        [Fact]
        public async Task GetCouch_InputInValidId_ReturnsNull()
        {
            //Arrange
            var couch = _dbContext.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + (couch.Id + 1);

            //Act
            var response = await _client.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory, AutoDomainData]
        public async Task PutCouch_InputCouchViewModel_ReturnsOkAndChangedCouchViewModel(CouchViewModel couchViewModel)
        {
            //Arrange
            var couch = _dbContext.CouchEntities.LastOrDefault();
            string route = RouteWithId + (couch!.Id);
            JsonContent content = JsonContent.Create(couchViewModel);

            //Act
            var response = await _client.PutAsync(route, content);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(couchViewModel.FirstName);
        }

        [Theory, AutoDomainData]
        public async Task PostCouch_InputCouchViewModel_ReturnsOkAndAddedCouchViewModel(CouchViewModel couchViewModel)
        {
            //Arrange
            JsonContent content = JsonContent.Create(couchViewModel);

            //Act
            var response = await _client.PostAsync(RouteWithoutId, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var couch = _dbContext.CouchEntities.LastOrDefault();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(couch!.FirstName);
        }

        [Fact]
        public async Task DeleteCouch_InputValidId_ReturnsNoContent()
        {
            //Arrange
            var couchEntity = _dbContext.CouchEntities.FirstOrDefault();
            string route = RouteWithId + couchEntity!.Id;

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCouch_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            var couchEntity = _dbContext.CouchEntities.LastOrDefault();
            string route = RouteWithId + (couchEntity!.Id + 2);

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        private IEnumerable<CouchEntity> GetCouchesEntityForTest()
        {
            return _fixture.Build<CouchEntity>().Without(p => p.Id).Without(p => p.Visitors).CreateMany(5).ToList();
        }

    }
}
