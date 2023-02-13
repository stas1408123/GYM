using AutoMapper;
using GYM.API.Models;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GYM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly IGenericService<VisitorModel> _visitorService;
        private readonly IMapper _mapper;

        public VisitorsController(IGenericService<VisitorModel> service, IMapper mapper)
        {
            _visitorService = service;
            _mapper = mapper;
        }

        // GET: api/Visitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorViewModel>>> GetVisitors()
        {
            var visitorsModel = await _visitorService.GetAll();

            return Ok(_mapper.Map<IEnumerable<VisitorModel>, IEnumerable<VisitorViewModel>>(visitorsModel));
        }

        // GET: api/Visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorViewModel>> GetVisitor(int id)
        {
            var visitorModel = await _visitorService.Get(id);
            if (visitorModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VisitorViewModel>(visitorModel));
        }

        // PUT: api/Visitors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(int id, VisitorViewModel visitorViewModel)
        {
            var visitorModel = _mapper.Map<VisitorModel>(visitorViewModel);
            visitorModel.Id = id;
            await _visitorService.Update(visitorModel);

            return Ok(visitorModel);
        }

        // POST: api/Visitors
        [HttpPost]
        public async Task<ActionResult<VisitorViewModel>> PostVisitor(VisitorViewModel visitorViewModel)
        {
            var visitorModel = _mapper.Map<VisitorModel>(visitorViewModel);
            await _visitorService.Create(visitorModel);

            return Ok(visitorModel);
        }

        // DELETE: api/Visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            if (await _visitorService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
