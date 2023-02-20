using AutoMapper;
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
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<IRepository<OrderEntity>> _repository;
        public OrderServiceTests()
        {
            _mapperMoq = new Mock<IMapper>();
            _repository = new Mock<IRepository<OrderEntity>>();
        }

        [Fact]
        public async Task GetALL_ReturnsOrderModels()
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            _repository.Setup(cr => cr.GetAll()).ReturnsAsync(TestEntities.GetOrderEntitiesForTest());

            _mapperMoq.Setup(m => m.Map<IEnumerable<OrderEntity>, IEnumerable<OrderModel>>(It.IsAny<IEnumerable<OrderEntity>>())).Returns(TestModels.GetOrderModelsForTest());

            //Act
            var resultOrdersModel = await orderService.GetAll();


            //Assert
            resultOrdersModel.ShouldAllBe(om => om.Cost > 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Get_InputValidId_ReturnsOrderModel(int id)
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetOrderEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<OrderModel>(It.IsAny<OrderEntity>())).Returns(TestModels.GetOrderModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var orderModelResult = await orderService.Get(id);

            //Assert
            orderModelResult.ShouldBeOfType(typeof(OrderModel));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public async Task Get_InputInvalidId_ReturnsNull(int id)
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            _repository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(TestEntities.GetOrderEntitiesForTest().FirstOrDefault(ce => ce.Id == id));

            _mapperMoq.Setup(m => m.Map<OrderModel>(It.IsAny<OrderEntity>())).Returns(TestModels.GetOrderModelsForTest().FirstOrDefault(cm => cm.Id == id)!);

            //Act
            var orderModelResult = await orderService.Get(id);

            //Assert
            orderModelResult.ShouldBeNull();
        }

        [Theory]
        [InlineData("Cross-fit #1")]
        [InlineData("Full-body program #2")]
        [InlineData("Cross-fit #5")]
        [InlineData("Full-body program #7")]
        public async Task Get_InputPredicate_ReturnsVisitorModels(string expectedTitle)
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
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
            var orderModelsResult = await orderService.Get(modelPredicate);

            //Assert
            orderModelsResult.ShouldContain(vm => vm.Title == expectedTitle);
        }

        [Theory]
        [AutoDomainData]
        public async Task Create_InputOrderModel_VerifyInvokeCreate(OrderEntity orderEntity, OrderModel orderModel)
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            _mapperMoq.Setup(m => m.Map<OrderEntity>(It.IsAny<OrderModel>())).Returns(orderEntity);

            //Act
            await orderService.Create(orderModel);

            //Assert
            _repository.Verify(p => p.Create(It.IsAny<OrderEntity>()), Times.Once);
        }

        [Theory]
        [AutoDomainData]
        public async Task Update_InputOrderModel_VerifyInvokeUpdate(OrderEntity orderEntity, OrderModel orderModel)
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            _mapperMoq.Setup(m => m.Map<OrderEntity>(It.IsAny<OrderModel>())).Returns(orderEntity);

            //Act
            await orderService.Update(orderModel);

            //Assert
            _repository.Verify(p => p.Update(It.IsAny<OrderEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InputValidId_ReturnsTrue()
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            var id = 1;
            var deleteResult = true;
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await orderService.Delete(id);

            //Assert
            result.ShouldBeTrue();
        }


        [Fact]
        public async Task Delete_InputInvalidId_ReturnsFalse()
        {
            //Arrange
            var orderService = new OrderService(_repository.Object, _mapperMoq.Object);
            var id = 4;
            var deleteResult = false;
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(deleteResult);

            //Act
            var result = await orderService.Delete(id);

            //Assert
            result.ShouldBeFalse();
        }
    }
}
