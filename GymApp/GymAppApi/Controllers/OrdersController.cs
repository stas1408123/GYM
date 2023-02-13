using AutoMapper;
using GYM.API.Models;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GYM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IGymService<OrderModel> _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IGymService<OrderModel> service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders()
        {
            var ordersModel = await _ordersService.GetAll();
            return Ok(_mapper.Map<IEnumerable<OrderModel>, IEnumerable<OrderViewModel>>(ordersModel));
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(int id)
        {
            var orderModel = await _ordersService.Get(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return Ok(orderModel);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderViewModel orderViewModelModel)
        {
            var orderModel = _mapper.Map<OrderModel>(orderViewModelModel);
            orderModel.Id = id;
            await _ordersService.Update(orderModel);

            return Ok(orderModel);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> PostOrder(OrderViewModel orderViewModel)
        {
            var orderModel = _mapper.Map<OrderModel>(orderViewModel);
            await _ordersService.Create(orderModel);

            return Ok(orderModel);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (await _ordersService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
