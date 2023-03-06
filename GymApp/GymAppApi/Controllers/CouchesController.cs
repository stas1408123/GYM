using AutoMapper;
using FluentValidation;
using GYM.API.Models;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GYM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CouchesController : ControllerBase
    {
        private readonly IGenericService<CouchModel> _couchService;
        private readonly IMapper _mapper;
        private readonly IValidator<CouchViewModel> _validator;


        public CouchesController(IGenericService<CouchModel> service, IMapper mapper, IValidator<CouchViewModel> validator)
        {
            _couchService = service;
            _mapper = mapper;
            _validator = validator;
        }

        // GET: api/Couches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouchViewModel>>> GetCouches()
        {
            var couchesModel = await _couchService.GetAll();

            return Ok(_mapper.Map<IEnumerable<CouchModel>, IEnumerable<CouchViewModel>>(couchesModel));
        }

        // GET: api/Couches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouchViewModel>> GetCouch(int id)
        {
            var couchModel = await _couchService.Get(id);
            if (couchModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CouchViewModel>(couchModel));
        }

        // PUT: api/Couches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouch(int id, CouchViewModel couchViewModel)
        {
            await _validator.ValidateAndThrowAsync(couchViewModel);
            var couchModel = _mapper.Map<CouchModel>(couchViewModel);
            couchModel.Id = id;
            await _couchService.Update(couchModel);

            return Ok(couchModel);
        }

        // POST: api/Couches
        [HttpPost]
        public async Task<IActionResult> PostCouch(CouchViewModel couchViewModel)
        {
            await _validator.ValidateAndThrowAsync(couchViewModel);
            var couchModel = _mapper.Map<CouchModel>(couchViewModel);
            await _couchService.Create(couchModel);

            return Ok(couchModel);
        }

        // DELETE: api/Couches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouch(int id)
        {
            if (await _couchService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
