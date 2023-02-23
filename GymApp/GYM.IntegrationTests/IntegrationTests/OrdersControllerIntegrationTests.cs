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
    public class OrdersControllerIntegrationTests : IClassFixture<GymWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly GymAppDbContext _dbContext;
        private readonly Fixture _fixture;
        private const string RouteWithoutId = "api/Orders";
        private const string RouteWithId = "api/Orders/";
        public OrdersControllerIntegrationTests(GymWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _dbContext = factory.Services.CreateScope().ServiceProvider.GetService<GymAppDbContext>()!;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetOrders_HasNotData_ReturnsStatusOkAndAllOrders()
        {
            //Arrange
            await InitializeDb();
            var orderEntity = _dbContext.OrderEntities.LastOrDefault();

            //Act
            var response = await _client.GetAsync(RouteWithoutId);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(orderEntity!.Title);
        }

        [Fact]
        public async Task GetOrder_InputValidId_ReturnsStatusOkAndOrder()
        {
            //Arrange
            await InitializeDb();
            var orderEntity = _dbContext.OrderEntities.LastOrDefault()!;

            //Act
            var response = await _client.GetAsync(RouteWithId + orderEntity.Id);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(orderEntity.Title);
        }

        [Fact]
        public async Task GetOrder_InputInValidId_ReturnsStatusNotFound()
        {
            //Arrange
            await InitializeDb();
            var orderEntity = _dbContext.OrderEntities.LastOrDefault()!;
            var route = RouteWithId + (orderEntity.Id + 1);

            //Act
            var response = await _client.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutOrder_InputOrderViewModel_ReturnsOkAndChangedViewModel()
        {
            //Arrange
            await InitializeDb();
            var orderViewModel = GetOrderViewModelForTest();
            var orderEntity = _dbContext.OrderEntities.LastOrDefault()!;
            string route = RouteWithId + (orderEntity.Id);
            JsonContent content = JsonContent.Create(orderViewModel);

            //Act
            var response = await _client.PutAsync(route, content);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(orderViewModel.Title);
        }

        [Fact]
        public async Task PostOrder_InputOrderViewModel_ReturnsOkAndAddedViewModel()
        {
            //Arrange
            await InitializeDb();
            var orderViewModel = GetOrderViewModelForTest();
            JsonContent content = JsonContent.Create(orderViewModel);

            //Act
            var response = await _client.PostAsync(RouteWithoutId, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var lastOrder = _dbContext.OrderEntities.LastOrDefault()!;

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseString.ShouldContain(orderViewModel.Title);
            lastOrder.Title.ShouldBe(orderViewModel.Title);
        }

        [Fact]
        public async Task DeleteOrder_InputValidId_ReturnsNoContent()
        {
            //Arrange
            await InitializeDb();
            var orderEntity = _dbContext.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + orderEntity.Id;

            //Act
            var response = await _client.DeleteAsync(route);
            var resultLastOrderEntity = _dbContext.OrderEntities.LastOrDefault()!;

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            resultLastOrderEntity.Id.ShouldNotBe(orderEntity.Id);
        }

        [Fact]
        public async Task DeleteOrder_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            await InitializeDb();
            var orderEntity = _dbContext.OrderEntities.LastOrDefault()!;
            string route = RouteWithId + (orderEntity.Id + 1);

            //Act
            var response = await _client.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        //initialize Db
        private async Task InitializeDb()
        {
            IEnumerable<OrderEntity> ordersEntities = _fixture.Build<OrderEntity>()
                .Without(p => p.Id)
                .Without(p => p.Visitor)
                //.With(p => p.VisitorId, 1)
                .CreateMany(5).ToList();

            _dbContext.OrderEntities.AddRange(ordersEntities);
            await _dbContext.SaveChangesAsync();
        }

        //Get random visitor
        private OrderViewModel GetOrderViewModelForTest()
        {
            return _fixture.Build<OrderViewModel>()
                .Without(p => p.Id)
                .Create();
        }
    }
}