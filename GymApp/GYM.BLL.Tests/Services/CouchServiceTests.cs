using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using GYM.BLL.Services;
using GYM.BLL.Tests.TestData;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace GYM.BLL.Tests.Services
{
    public class CouchServiceTests
    {
        private readonly IGenericService<CouchModel> _couchService;
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<IRepository<CouchEntity>> _repository;

        public CouchServiceTests()
        {
            _mapperMoq = new Mock<IMapper>();
            _repository = new Mock<IRepository<CouchEntity>>();
            _couchService = new Mock<CouchService>(_repository.Object, _mapperMoq.Object).Object;
        }

        [Fact]
        public async Task GetALL_CheckNullVisitors_ReturnsCouchesModels()
        {
            //Arrange
            _repository.Setup(cr => cr.GetAll()).ReturnsAsync(TestEntities.GetCouchEntitiesForTest());

            _mapperMoq.Setup(m => m.Map<IEnumerable<CouchEntity>, IEnumerable<CouchModel>>(It.IsAny<IEnumerable<CouchEntity>>())).Returns(TestModels.GetCouchModelsForTest());

            //Act
            var resultCouchesModel = await _couchService.GetAll();

            //Assert
            resultCouchesModel.ShouldAllBe(c => c.Visitors == null);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Get_InputValidIdCheckType_ReturnsCouchesModel(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetCouchEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<CouchModel>(It.IsAny<CouchEntity>())).Returns(TestModels.GetCouchModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var resultCouchesModel = await _couchService.Get(id);

            //Assert
            resultCouchesModel.ShouldBeOfType(typeof(CouchModel));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public async Task Get_InputInvalidIdCheckNull_ReturnsNull(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetCouchEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<CouchModel>(It.IsAny<CouchEntity>())).Returns(TestModels.GetCouchModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var resultCouchesModel = await _couchService.Get(id);

            //Assert
            resultCouchesModel.ShouldBeNull();
        }

        [Theory]
        [InlineData("Arnold")]
        [InlineData("Mick")]
        [InlineData("Olivia")]
        public async Task Get_InputPredicateCheckName_ReturnsCouchesModel(string expectedName)
        {
            //Arrange
            Expression<Func<CouchEntity, bool>> predicateEntity = (pe) => pe.FirstName == expectedName;
            Expression<Func<CouchModel, bool>> modelPredicate = (mp) => mp.FirstName == expectedName;

            _repository.Setup(cr => cr.Get(It.IsAny<Expression<Func<CouchEntity, bool>>>())).ReturnsAsync(TestEntities.GetCouchEntitiesForTest());

            _mapperMoq.Setup(m =>
                    m.Map<Expression<Func<CouchEntity, bool>>>(It.IsAny<Expression<Func<CouchModel, bool>>>()))
                .Returns(predicateEntity);

            _mapperMoq.Setup(m => m.Map<IEnumerable<CouchModel>>(It.IsAny<IEnumerable<CouchEntity>>()
            )).Returns(TestModels.GetCouchModelsForTest().Where(ce => ce.FirstName == expectedName));

            _mapperMoq.Setup(m => m.Map<IEnumerable<CouchEntity>, IEnumerable<CouchModel>>(It.IsAny<IEnumerable<CouchEntity>>()))
                .Returns(TestModels.GetCouchModelsForTest().Where(ce => ce.FirstName == expectedName));

            //Act
            var resultCouchesModel = await _couchService.Get(modelPredicate);

            //Assert
            resultCouchesModel.ShouldAllBe(c => c.FirstName == expectedName);
        }

        [Fact]
        public async Task Create_InputCouchModelSaveCouchEntity_VerifyInvokeCreate()
        {
            //Arrange
            var id = 1;
            var couchEntity = TestEntities.GetCouchEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var couchModel = TestModels.GetCouchModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<CouchEntity>(It.IsAny<CouchModel>())).Returns(couchEntity);

            //Act
            await _couchService.Create(couchModel);

            //Assert
            _repository.Verify(p => p.Create(It.IsAny<CouchEntity>()), Times.Once);
        }


        [Fact]
        public async Task Update_InputCouchModelUpdateCouchEntity_VerifyInvokeUpdate()
        {
            //Arrange
            var id = 1;
            var couchEntity = TestEntities.GetCouchEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var couchModel = TestModels.GetCouchModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<CouchEntity>(It.IsAny<CouchModel>())).Returns(couchEntity);

            //Act
            await _couchService.Update(couchModel);

            //Assert
            _repository.Verify(p => p.Update(It.IsAny<CouchEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InputValidId_ReturnsTrue()
        {
            //Arrange
            var id = 1;
            var deleteResult = true;
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _couchService.Delete(id);

            //Assert
            result.ShouldBeTrue();
        }


        [Fact]
        public async Task Delete_InputInvalidId_ReturnsFalse()
        {
            //Arrange
            var id = 1;
            var deleteResult = false;

            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _couchService.Delete(id);

            //Assert
            result.ShouldBeFalse();
        }
    }
}
