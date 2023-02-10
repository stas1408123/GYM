using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace GYM.BLL.Services
{
    public class VisitorService : IGymService<VisitorModel>
    {
        private readonly IRepository<VisitorEntity> _repository;
        private readonly IMapper _mapper;

        public VisitorService(IRepository<VisitorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(VisitorModel item)
        {
            var orderEntity = _mapper.Map<VisitorEntity>(item);
            await _repository.Create(orderEntity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<VisitorModel>> Find(Expression<Func<VisitorModel, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<VisitorEntity, bool>>>(predicate);
            var couchEntities = await _repository.Find(entityPredicate);
            return _mapper.Map<IEnumerable<VisitorModel>>(couchEntities);
        }

        public async Task<VisitorModel?> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<VisitorModel?>(entity);
        }

        public async Task<IEnumerable<VisitorModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<VisitorEntity>, IEnumerable<VisitorModel>>(entities);
        }

        public async Task Update(VisitorModel item)
        {
            var entity = _mapper.Map<VisitorEntity>(item);
            await _repository.Update(entity);
        }
    }
}
