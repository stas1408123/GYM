using AutoFixture;
using GYM.API.Models;
using GYM.DAL.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace GYM.API.IntegrationTests.IntegrationTests
{
    public class CouchesControllerIntegrationTests : IntegrationTestsBase
    {
        private const string RouteWithoutId = "api/Couches";
        private const string RouteWithId = "api/Couches/";

        public CouchesControllerIntegrationTests()
        {
            DbContextForTests.CouchEntities.AddRange(GetCouchesEntityForTest());
            DbContextForTests.SaveChanges();
        }

        [Fact]
        public async Task GetCouches_HasNotData_ReturnsStatusOkAndAllCouches()
        {
            //Arrange
            var couch = DbContextForTests.CouchEntities.LastOrDefault();

            //Act
            var response = await ClientForTests.GetAsync(RouteWithoutId);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<CouchViewModel>>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.LastOrDefault()!.FirstName.ShouldBe(couch!.FirstName);
        }

        [Fact]
        public async Task GetCouch_InputValidId_ReturnsStatusOkAndCouch()
        {
            //Arrange
            var couch = DbContextForTests.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + couch.Id;

            //Act
            var response = await ClientForTests.GetAsync(route);
            var resultViewModel = await response.Content.ReadFromJsonAsync<CouchViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            resultViewModel!.FirstName.ShouldBe(couch.FirstName);
        }

        [Fact]
        public async Task GetCouch_InputInValidId_ReturnsNull()
        {
            //Arrange
            var couch = DbContextForTests.CouchEntities.LastOrDefault()!;
            string route = RouteWithId + (couch.Id + 1);

            //Act
            var response = await ClientForTests.GetAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory, AutoDomainData]
        public async Task PutCouch_InputCouchViewModel_ReturnsOkAndChangedCouchViewModel(CouchViewModel couchViewModel)
        {
            //Arrange
            var couch = DbContextForTests.CouchEntities.LastOrDefault();
            string route = RouteWithId + (couch!.Id);
            JsonContent content = JsonContent.Create(couchViewModel);

            //Act
            var response = await ClientForTests.PutAsync(route, content);
            var result = await response.Content.ReadFromJsonAsync<CouchViewModel>();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.FirstName.ShouldBe(couchViewModel.FirstName);
        }

        [Theory, AutoDomainData]
        public async Task PostCouch_InputCouchViewModel_ReturnsOkAndAddedCouchViewModel(CouchViewModel couchViewModel)
        {
            //Arrange
            JsonContent content = JsonContent.Create(couchViewModel);

            //Act
            var response = await ClientForTests.PostAsync(RouteWithoutId, content);
            var result = await response.Content.ReadFromJsonAsync<CouchViewModel>();
            var couch = DbContextForTests.CouchEntities.LastOrDefault();

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result!.FirstName.ShouldBe(couch!.FirstName);
        }

        [Fact]
        public async Task DeleteCouch_InputValidId_ReturnsNoContent()
        {
            //Arrange
            var couchEntity = DbContextForTests.CouchEntities.FirstOrDefault();
            string route = RouteWithId + couchEntity!.Id;

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCouch_InputInValidId_ReturnsNotFound()
        {
            //Arrange
            var couchEntity = DbContextForTests.CouchEntities.LastOrDefault();
            string route = RouteWithId + (couchEntity!.Id + 2);

            //Act
            var response = await ClientForTests.DeleteAsync(route);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test data
        private IEnumerable<CouchEntity> GetCouchesEntityForTest()
        {
            return FixtureForTests.Build<CouchEntity>().Without(p => p.Id).Without(p => p.Visitors).CreateMany(5).ToList();
        }

    }
}
