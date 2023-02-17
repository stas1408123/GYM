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
    public class OrderServiceTests
    {
        private readonly IGenericService<OrderModel> _orderService;
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<IRepository<OrderEntity>> _repository;
        public OrderServiceTests()
        {
            _mapperMoq = new Mock<IMapper>();
            _repository = new Mock<IRepository<OrderEntity>>();
            _orderService = new Mock<OrderService>(_repository.Object, _mapperMoq.Object).Object;
        }

        [Fact]
        public async Task GetALL_ReturnsAllOrdersModel_CheckCostGreaterZero()
        {
            //Arrange
            _repository.Setup(cr => cr.GetAll()).ReturnsAsync(TestEntities.GetOrderEntitiesForTest());

            _mapperMoq.Setup(m => m.Map<IEnumerable<OrderEntity>, IEnumerable<OrderModel>>(It.IsAny<IEnumerable<OrderEntity>>())).Returns(TestModels.GetOrderModelsForTest());

            //Act
            var resultOrdersModel = await _orderService.GetAll();


            //Assert
            resultOrdersModel.ShouldAllBe(om => om.Cost > 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Get_ReturnsOrderModel_CheckType(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetOrderEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<OrderModel>(It.IsAny<OrderEntity>())).Returns(TestModels.GetOrderModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var orderModelResult = await _orderService.Get(id);

            //Assert
            orderModelResult.ShouldBeOfType(typeof(OrderModel));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public async Task Get_ReturnsNull_CheckOnNull(int id)
        {
            //Arrange
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetOrderEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<OrderModel>(It.IsAny<OrderEntity>())).Returns(TestModels.GetOrderModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var orderModelResult = await _orderService.Get(id);

            //Assert
            orderModelResult.ShouldBeNull();
        }

        [Theory]
        [InlineData("Cross-fit #1")]
        [InlineData("Full-body program #2")]
        [InlineData("Cross-fit #5")]
        [InlineData("Full-body program #7")]
        public async Task Get_ReturnsVisitorModelByPredicate_CheckTitle(string expectedTitle)
        {
            //Arrange
            Expression<Func<OrderEntity, bool>> entityPredicate = (pe) => pe.Title == expectedTitle;
            Expression<Func<OrderModel, bool>> modelPredicate = (mp) => mp.Title == expectedTitle;

            _repository.Setup(vr => vr.Get(It.IsAny<Expression<Func<OrderEntity, bool>>>())).ReturnsAsync(TestEntities.GetOrderEntitiesForTest());

            _mapperMoq.Setup(m =>
                    m.Map<Expression<Func<OrderEntity, bool>>>(It.IsAny<Expression<Func<OrderModel, bool>>>()))
                .Returns(entityPredicate);

            _mapperMoq.Setup(m => m.Map<IEnumerable<OrderModel>>(It.IsAny<IEnumerable<OrderEntity>>()
            )).Returns(TestModels.GetOrderModelsForTest().Where(ce => ce.Title == expectedTitle));

            _mapperMoq.Setup(m => m.Map<IEnumerable<OrderEntity>, IEnumerable<OrderModel>>(It.IsAny<IEnumerable<OrderEntity>>())).Returns(TestModels.GetOrderModelsForTest().Where(ce => ce.Title == expectedTitle));

            //Act
            var orderModelsResult = await _orderService.Get(modelPredicate);

            //Assert
            orderModelsResult.ShouldContain(vm => vm.Title == expectedTitle);
        }

        [Fact]
        public async Task Create_InputOrderModelSaveOrderEntity_VerifyInvokeCreate()
        {
            //Arrange
            int id = 1;
            var orderEntity = TestEntities.GetOrderEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var orderModel = TestModels.GetOrderModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<OrderEntity>(It.IsAny<OrderModel>())).Returns(orderEntity);

            //Act
            await _orderService.Create(orderModel);

            //Assert
            _repository.Verify(p => p.Create(It.IsAny<OrderEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_InputOrderModelSaveOrderEntity_VerifyInvokeUpdate()
        {
            //Arrange
            int id = 1;
            var orderEntity = TestEntities.GetOrderEntitiesForTest().FirstOrDefault(e => e.Id == id)!;
            var orderModel = TestModels.GetOrderModelsForTest().FirstOrDefault(e => e.Id == id)!;

            _mapperMoq.Setup(m => m.Map<OrderEntity>(It.IsAny<OrderModel>())).Returns(orderEntity);

            //Act
            await _orderService.Update(orderModel);

            //Assert
            _repository.Verify(p => p.Update(It.IsAny<OrderEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsPositiveResult()
        {
            //Arrange
            int id = 1;
            bool deleteResult = true;

            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _orderService.Delete(id);

            //Assert
            result.ShouldBeTrue();
        }


        [Fact]
        public async Task Delete_ReturnsNegativeResult()
        {
            //Arrange
            int id = 1;
            bool deleteResult = false;

            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await _orderService.Delete(id);

            //Assert
            result.ShouldBeFalse();
        }
    }
}
