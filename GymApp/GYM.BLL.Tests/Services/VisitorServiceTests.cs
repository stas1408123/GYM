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
    public class VisitorServiceTests
    {
        private readonly IGenericService<VisitorModel> _visitorService;
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<IRepository<VisitorEntity>> _repository;

        public VisitorServiceTests()
        {
            _mapperMoq = new Mock<IMapper>();
            _repository = new Mock<IRepository<VisitorEntity>>();
            _visitorService = new Mock<VisitorService>(_repository.Object, _mapperMoq.Object).Object;
        }

        [Fact]
        public async Task GetALL_CheckCountCouchesAndOrders_ReturnsVisitorModels()
        {
            //Arrange
            _repository.Setup(cr => cr.GetAll()).ReturnsAsync(TestEntities.GetVisitorEntitiesForTest());

            _mapperMoq.Setup(m =>
                    m.Map<IEnumerable<VisitorEntity>, IEnumerable<VisitorModel>>(
                        It.IsAny<IEnumerable<VisitorEntity>>()))
                .Returns(TestModels.GetVisitorModelsForTest);

            //Act
            var resultVisitorModel = await _visitorService.GetAll();

            //Assert
            resultVisitorModel.ShouldAllBe(v => v.Couches!.Count == 0 && v.Orders!.Count == 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Get_InputValidIdCheckType_ReturnsVisitorModel(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetVisitorEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<VisitorModel>(It.IsAny<VisitorEntity>())).Returns(TestModels.GetVisitorModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var visitorModelResult = await _visitorService.Get(id);

            //Assert
            visitorModelResult.ShouldBeOfType(typeof(VisitorModel));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public async Task Get_InputInvalidIdCheckNull_ReturnsNull(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetVisitorEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<VisitorModel>(It.IsAny<VisitorEntity>())).Returns(TestModels.GetVisitorModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var visitorModelResult = await _visitorService.Get(id);

            //Assert
            visitorModelResult.ShouldBeNull();
        }

        [Theory]
        [InlineData("Connor")]
        [InlineData("Margaret")]
        [InlineData("Emma")]
        public async Task Get_InputPredicateCheckName_ReturnsVisitorModels(string name)
        {
            //Arrange
            Expression<Func<VisitorEntity, bool>> predicateEntity = (pe) => pe.FirstName == name;
            Expression<Func<VisitorModel, bool>> modelPredicate = (mp) => mp.FirstName == name;

            _repository.Setup(vr => vr.Get(It.IsAny<Expression<Func<VisitorEntity, bool>>>())).ReturnsAsync(TestEntities.GetVisitorEntitiesForTest);

            _mapperMoq.Setup(m =>
                    m.Map<Expression<Func<VisitorEntity, bool>>>(It.IsAny<Expression<Func<VisitorModel, bool>>>()))
                .Returns(predicateEntity);

            _mapperMoq.Setup(m => m.Map<IEnumerable<VisitorModel>>(It.IsAny<IEnumerable<VisitorEntity>>()
            )).Returns(TestModels.GetVisitorModelsForTest().Where(ce => ce.FirstName == name));

            _mapperMoq.Setup(m => m.Map<IEnumerable<VisitorEntity>, IEnumerable<VisitorModel>>(It.IsAny<IEnumerable<VisitorEntity>>())).Returns(TestModels.GetVisitorModelsForTest().Where(ce => ce.FirstName == name));

            //Act
            var visitorModelResult = await _visitorService.Get(modelPredicate);

            //Assert
            visitorModelResult.ShouldContain(vm => vm.FirstName == name);
        }

        [Fact]
        public async Task Create_InputVisitorModelSaveVisitorEntity_VerifyInvokeCreate()
        {
            //Arrange
            var id = 1;
            var visitorEntity = TestEntities.GetVisitorEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var visitorModel = TestModels.GetVisitorModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<VisitorEntity>(It.IsAny<VisitorServiceTests>())).Returns(visitorEntity);

            //Act
            await _visitorService.Create(visitorModel);

            //Assert
            _repository.Verify(p => p.Create(It.IsAny<VisitorEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_InputCouchModelUpdateCouchEntity_VerifyInvokeUpdate()
        {
            //Arrange
            var id = 1;
            var visitorEntity = TestEntities.GetVisitorEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var visitorModel = TestModels.GetVisitorModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<VisitorEntity>(It.IsAny<VisitorModel>())).Returns(visitorEntity);

            //Act
            await _visitorService.Update(visitorModel);

            //Assert
            _repository.Verify(p => p.Update(It.IsAny<VisitorEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InputValidId_ReturnsTrue()
        {
            //Arrange
            var id = 1;
            var deleteResult = true;

            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _visitorService.Delete(id);

            //Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task Delete_InputInvalidId_ReturnsFalse()
        {
            //Arrange
            var id = 5;
            var deleteResult = false;

            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _visitorService.Delete(id);

            //Assert
            result.ShouldBeFalse();
        }
    }
}
