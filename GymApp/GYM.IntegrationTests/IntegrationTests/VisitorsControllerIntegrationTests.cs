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
    public class VisitorsControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly GymAppDbContext _dbContext;
        private readonly Fixture _fixture;
        private const string RouteWithoutId = "api/Visitors";
        private const string RouteWithId = "api/Visitors/";

        public VisitorsControllerIntegrationTests()
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
            _dbContext.VisitorEntities.AddRange(GetVisitorEntitiesForTests());
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetVisitors_HasNotData_ReturnsStatusOkAndAllVisitors()
        {
            //Arrange
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();

            //Act
            var response = await _client.GetAsync(RouteWithoutId);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(visitorEntity!.FirstName);
        }

        [Fact]
        public async Task GetVisitor_InputValidId_ReturnsStatusOkAndVisitor()
        {
            //Arrange
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault()!;
            string route = RouteWithId + visitorEntity.Id;

            //Act
            var response = await _client.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(visitorEntity.FirstName);
        }

        [Fact]
        public async Task GetVisitor_InputInValidId_ReturnsStatusNotFound()
        {
            //Arrange
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault()!;
            string route = RouteWithId + (visitorEntity.Id + 1);

            //Act
            var response = await _client.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task PutVisitor_InputVisitorViewModel_ReturnsOkAndChangedVisitorViewModel()
        {
            //Arrange
            var visitorViewModel = GetVisitorViewModelForTest();
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();
            string route = RouteWithId + (visitorEntity!.Id);
            JsonContent content = JsonContent.Create(visitorViewModel);

            //Act
            var response = await _client.PutAsync(route, content);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(visitorViewModel.FirstName);
        }

        [Fact]
        public async Task PostVisitor_InputVisitorViewModel_ReturnsOkAndAddedVisitor()
        {
            //Arrange
            var visitorViewModel = GetVisitorViewModelForTest();
            JsonContent content = JsonContent.Create(visitorViewModel);

            //Act
            var response = await _client.PostAsync(RouteWithoutId, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(visitorEntity!.FirstName);
        }

        [Fact]
        public async Task DeleteVisitor_InputValidId_ReturnsNoContent()
        {
            //Arrange
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();
            string route = RouteWithId + visitorEntity!.Id;

            //Act
            var response = await _client.DeleteAsync(route);
            var resultLastVisitor = _dbContext.VisitorEntities.LastOrDefault()!;

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            resultLastVisitor.Id.ShouldNotBe(visitorEntity.Id);
        }

        [Fact]
        public async Task DeleteCouch_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();
            string route = RouteWithId + (visitorEntity!.Id + 5);

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data

        private IEnumerable<VisitorEntity> GetVisitorEntitiesForTests()
        {
            return _fixture.Build<VisitorEntity>()
                  .Without(p => p.Id)
                  .With(p => p.Orders, new List<OrderEntity>())
                  .With(p => p.Couches, new List<CouchEntity>())
                  .CreateMany(5);
        }

        //Get random visitor
        private VisitorViewModel GetVisitorViewModelForTest()
        {
            return _fixture.Build<VisitorViewModel>().Without(p => p.Id).Without(p => p.Id)
                .With(p => p.Orders, new List<OrderViewModel>())
                .With(p => p.Couches, new List<CouchViewModel>())
                .Create();
        }
    }
}
