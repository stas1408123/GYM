using AutoFixture;
using GYM.API.Models;
using GYM.DAL.EF;
using GYM.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class VisitorsControllerIntegrationTests : IClassFixture<GymWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly GymAppDbContext _dbContext;
        private readonly Fixture _fixture;
        private const string RouteWithoutId = "api/Visitors";
        private const string RouteWithId = "api/Visitors/";

        public VisitorsControllerIntegrationTests(GymWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _dbContext = factory.Services.CreateScope().ServiceProvider.GetService<GymAppDbContext>()!;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetVisitors_HasNotData_ReturnsStatusOkAndAllVisitors()
        {
            //Arrange
            await InitializeDb();
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
            await InitializeDb();
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
            await InitializeDb();
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
            await InitializeDb();
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
            await InitializeDb();
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
            await InitializeDb();

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
            await InitializeDb();

            var visitorEntity = _dbContext.VisitorEntities.LastOrDefault();
            string route = RouteWithId + (visitorEntity!.Id + 1);

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        //initialize Db
        private async Task InitializeDb()
        {
            var visitors = _fixture.Build<VisitorEntity>()
                .Without(p => p.Id)
                .With(p => p.Orders, new List<OrderEntity>())
                .With(p => p.Couches, new List<CouchEntity>())
                .CreateMany(5);
            _dbContext.VisitorEntities.AddRange(visitors);
            await _dbContext.SaveChangesAsync();
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
