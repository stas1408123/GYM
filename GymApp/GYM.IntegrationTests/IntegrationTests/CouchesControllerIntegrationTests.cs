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
    public class CouchesControllerIntegrationTests : IClassFixture<GymWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly GymAppDbContext _dbContext;
        private readonly Fixture _fixture;
        private const string RouteWithoutId = "api/Couches";
        private const string RouteWithId = "api/Couches/";

        public CouchesControllerIntegrationTests(GymWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _dbContext = factory.Services.CreateScope().ServiceProvider.GetService<GymAppDbContext>()!;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetCouches_HasNotData_ReturnsStatusOkAndAllCouches()
        {
            //Arrange
            await InitializeDb();
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
            await InitializeDb();
            var couch = _dbContext.CouchEntities.LastOrDefault()!;
            string route = "api/Couches/" + couch.Id;

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
            await InitializeDb();
            var couch = _dbContext.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + (couch.Id + 1);

            //Act
            var response = await _client.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutCouch_InputCouchViewModel_ReturnsOkAndChangedCouchViewModel()
        {
            //Arrange
            await InitializeDb();

            var couchViewModel = _fixture.Build<CouchViewModel>().Without(p => p.Id).With(p => p.Visitors, new List<VisitorViewModel>()).Create();

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

        [Fact]
        public async Task PostCouch_InputCouchViewModel_ReturnsOk()
        {
            //Arrange
            await InitializeDb();

            var couchViewModel = _fixture.Build<CouchViewModel>().Without(p => p.Id).With(p => p.Visitors, new List<VisitorViewModel>()).Create();

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
            await InitializeDb();

            var couchEntity = _dbContext.CouchEntities.LastOrDefault();
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
            await InitializeDb();

            var couchEntity = _dbContext.CouchEntities.LastOrDefault();
            string route = RouteWithId + (couchEntity!.Id + 1);

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //initialize Db
        private async Task InitializeDb()
        {
            IEnumerable<CouchEntity> couches = _fixture.Build<CouchEntity>().Without(p => p.Id).Without(p => p.Visitors).CreateMany(5).ToList();

            _dbContext.CouchEntities.AddRange(couches);
            await _dbContext.SaveChangesAsync();
        }
    }
}
