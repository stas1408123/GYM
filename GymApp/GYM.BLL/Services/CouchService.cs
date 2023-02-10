using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace GYM.BLL.Services
{
    public class CouchService : IGymService<CouchModel>
    {
        private readonly IRepository<CouchEntity> _repository;
        private readonly IMapper _mapper;
        public CouchService(IRepository<CouchEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Create(CouchModel item)
        {
            var couchEntity = _mapper.Map<CouchEntity>(item);
            await _repository.Create(couchEntity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<CouchModel>> Find(Expression<Func<CouchModel, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<CouchEntity, bool>>>(predicate);
            var couchEntities = await _repository.Find(entityPredicate);
            return _mapper.Map<IEnumerable<CouchModel>>(couchEntities);
        }

        public async Task<CouchModel?> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<CouchModel?>(entity);
        }

        public async Task<IEnumerable<CouchModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<CouchEntity>, IEnumerable<CouchModel>>(entities);
        }

        public async Task Update(CouchModel item)
        {
            var entity = _mapper.Map<CouchEntity>(item);
            await _repository.Update(entity);
        }
    }
}
