using AutoFixture;
using GYM.API.Models;
using GYM.DAL.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class VisitorsControllerIntegrationTests : IntegrationTestsBase
    {
        private const string RouteWithoutId = "api/Visitors";
        private const string RouteWithId = "api/Visitors/";

        public VisitorsControllerIntegrationTests()
        {
            DbContextForTests.VisitorEntities.AddRange(GetVisitorEntitiesForTests());
            DbContextForTests.SaveChanges();
        }

        [Fact]
        public async Task GetVisitors_HasNotData_ReturnsStatusOkAndAllVisitors()
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault();

            //Act
            var response = await ClientForTests.GetAsync(RouteWithoutId);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<VisitorViewModel>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.LastOrDefault()!.FirstName.ShouldBe(visitorEntity!.FirstName);
        }

        [Fact]
        public async Task GetVisitor_InputValidId_ReturnsStatusOkAndVisitor()
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault()!;
            string route = RouteWithId + visitorEntity.Id;

            //Act
            var response = await ClientForTests.GetAsync(route);
            var result = await response.Content.ReadFromJsonAsync<VisitorViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.FirstName.ShouldBe(visitorEntity.FirstName);
        }

        [Fact]
        public async Task GetVisitor_InputInValidId_ReturnsStatusNotFound()
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault()!;
            string route = RouteWithId + (visitorEntity.Id + 1);

            //Act
            var response = await ClientForTests.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }


        [Theory, AutoDomainData]
        public async Task PutVisitor_InputVisitorViewModel_ReturnsOkAndChangedVisitorViewModel(VisitorViewModel visitorViewModel)
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault();
            string route = RouteWithId + (visitorEntity!.Id);
            JsonContent content = JsonContent.Create(visitorViewModel);

            //Act
            var response = await ClientForTests.PutAsync(route, content);
            var result = await response.Content.ReadFromJsonAsync<VisitorViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.FirstName.ShouldBe(visitorViewModel.FirstName);
        }

        [Theory, AutoDomainData]
        public async Task PostVisitor_InputVisitorViewModel_ReturnsOkAndAddedVisitor(VisitorViewModel visitorViewModel)
        {
            //Arrange
            JsonContent content = JsonContent.Create(visitorViewModel);

            //Act
            var response = await ClientForTests.PostAsync(RouteWithoutId, content);
            var result = await response.Content.ReadFromJsonAsync<VisitorViewModel>();
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.FirstName.ShouldBe(visitorEntity!.FirstName);
        }

        [Fact]
        public async Task DeleteVisitor_InputValidId_ReturnsNoContent()
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.FirstOrDefault();
            string route = RouteWithId + visitorEntity!.Id;

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCouch_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            var visitorEntity = DbContextForTests.VisitorEntities.LastOrDefault();
            string route = RouteWithId + (visitorEntity!.Id + 5);

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        private IEnumerable<VisitorEntity> GetVisitorEntitiesForTests()
        {
            return FixtureForTests.Build<VisitorEntity>()
                  .Without(p => p.Id)
                  .With(p => p.Orders, new List<OrderEntity>())
                  .With(p => p.Couches, new List<CouchEntity>())
                  .CreateMany(5);
        }
    }
}
