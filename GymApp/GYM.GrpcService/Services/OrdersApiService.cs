using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Mapster;

namespace GYM.GrpcService.Services
{
    /// <summary>
    /// CRUD service for orders
    /// </summary>
    public class OrdersApiService : OrdersService.OrdersServiceBase
    {
        private readonly IGenericService<OrderModel> _orderService;

        /// <summary>
        /// Constructor for OrderService
        /// </summary>
        /// <param name="service"></param>
        public OrdersApiService(IGenericService<OrderModel> service)
        {
            _orderService = service;
        }

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<OrderReply> GetOrder(IdOrderRequest request, ServerCallContext context)
        {
            var order = await _orderService.Get(request.Id);
            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
            }
            return await Task.FromResult(request.Adapt<OrderReply>());
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ListOrdersReply> GetOrders(Empty request, ServerCallContext context)
        {
            var listOrdersReply = new ListOrdersReply();
            var ordersModel = await _orderService.GetAll();
            listOrdersReply.Orders.AddRange(
                ordersModel.Select(item => new OrderReply
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Cost = item.Cost,
                    Date = Timestamp.FromDateTime(item.Date),
                    VisitorId = item.VisitorId
                }
                ));
            return await Task.FromResult(listOrdersReply);
        }

        /// <summary>
        /// Create new order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Empty> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            await _orderService.Create(request.Adapt<OrderModel>());
            return await base.CreateOrder(request, context);
        }

        /// <summary>
        /// Update current order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<OrderReply> UpdateOrder(UpdateOrderRequest request, ServerCallContext context)
        {
            var order = await _orderService.Get(request.Id);
            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
            }

            await _orderService.Update(request.Adapt<OrderModel>());

            return await Task.FromResult(request.Adapt<OrderReply>());
        }

        /// <summary>
        /// Delete order by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<OrderReply> DeleteOrder(IdOrderRequest request, ServerCallContext context)
        {
            var order = await _orderService.Get(request.Id);
            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
            }

            await _orderService.Delete(request.Id);
            return await Task.FromResult(order.Adapt<OrderReply>());

        }
    }
}
