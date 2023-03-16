namespace GYM.API.Controllers
{
    [Authorize("AllMethodsAllowed")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IGenericService<OrderModel> _ordersService;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderViewModel> _validator;

        public OrdersController(IGenericService<OrderModel> service, IMapper mapper, IValidator<OrderViewModel> validator)
        {
            _ordersService = service;
            _mapper = mapper;
            _validator = validator;
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

            return Ok(_mapper.Map<OrderViewModel>(orderModel));
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderViewModel orderViewModelModel)
        {
            await _validator.ValidateAndThrowAsync(orderViewModelModel);
            var orderModel = _mapper.Map<OrderModel>(orderViewModelModel);
            orderModel.Id = id;
            await _ordersService.Update(orderModel);

            return Ok(orderModel);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> PostOrder(OrderViewModel orderViewModel)
        {
            await _validator.ValidateAndThrowAsync(orderViewModel);
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
