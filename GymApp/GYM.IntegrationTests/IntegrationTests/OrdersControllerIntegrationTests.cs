using AutoFixture;
using GYM.API.Models;
using GYM.DAL.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class OrdersControllerIntegrationTests : IntegrationTestsBase
    {
        private const string RouteWithoutId = "api/Orders";
        private const string RouteWithId = "api/Orders/";
        public OrdersControllerIntegrationTests()
        {
            DbContextForTests.OrderEntities.AddRange(GetOrderEntitiesForTest());
            DbContextForTests.SaveChanges();
        }

        [Fact]
        public async Task GetOrders_HasNotData_ReturnsStatusOkAndAllOrders()
        {
            //Arrange
            var orderEntity = DbContextForTests.OrderEntities.LastOrDefault();

            //Act
            var response = await ClientForTests.GetAsync(RouteWithoutId);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderViewModel>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.LastOrDefault()!.Title.ShouldBe(orderEntity!.Title);
        }

        [Fact]
        public async Task GetOrder_InputValidId_ReturnsStatusOkAndOrder()
        {
            //Arrange
            var orderEntity = DbContextForTests.OrderEntities.LastOrDefault()!;

            //Act
            var response = await ClientForTests.GetAsync(RouteWithId + orderEntity.Id);
            var result = await response.Content.ReadFromJsonAsync<OrderViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.Title.ShouldBe(orderEntity.Title);
        }

        [Fact]
        public async Task GetOrder_InputInValidId_ReturnsStatusNotFound()
        {
            //Arrange
            var orderEntity = DbContextForTests.OrderEntities.LastOrDefault()!;
            var route = RouteWithId + (orderEntity.Id + 2);

            //Act
            var response = await ClientForTests.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory, AutoDomainData]
        public async Task PutOrder_InputOrderViewModel_ReturnsOkAndChangedViewModel(OrderViewModel orderViewModel)
        {
            //Arrange
            var orderEntity = DbContextForTests.OrderEntities.LastOrDefault()!;
            string route = RouteWithId + (orderEntity.Id);
            JsonContent content = JsonContent.Create(orderViewModel);

            //Act
            var response = await ClientForTests.PutAsync(route, content);
            var result = await response.Content.ReadFromJsonAsync<OrderViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.Title.ShouldBe(orderViewModel.Title);
        }

        [Theory, AutoDomainData]
        public async Task PostOrder_InputOrderViewModel_ReturnsOkAndAddedViewModel(OrderViewModel orderViewModel)
        {
            //Arrange
            JsonContent content = JsonContent.Create(orderViewModel);

            //Act
            var response = await ClientForTests.PostAsync(RouteWithoutId, content);
            var result = await response.Content.ReadFromJsonAsync<OrderViewModel>();
            var lastOrder = DbContextForTests.OrderEntities.LastOrDefault()!;

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.Title.ShouldBe(lastOrder.Title);
        }

        [Fact]
        public async Task DeleteOrder_InputValidId_ReturnsNoContent()
        {
            //Arrange
            var orderEntity = DbContextForTests.CouchEntities.FirstOrDefault()!;
            string route = RouteWithId + orderEntity.Id;

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteOrder_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            var orderEntity = DbContextForTests.OrderEntities.LastOrDefault()!;
            string route = RouteWithId + (orderEntity.Id + 7);

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        private IEnumerable<OrderEntity> GetOrderEntitiesForTest()
        {
            return FixtureForTests.Build<OrderEntity>()
                 .Without(p => p.Id)
                 .Without(p => p.Visitor)
                 .CreateMany(5).ToList();
        }
    }
}