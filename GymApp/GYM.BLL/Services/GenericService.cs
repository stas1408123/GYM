using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace GYM.BLL.Services
{
    public class GenericService<TEntity, TModel> : IGymService<TModel>
        where TModel : class
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        }

        public async Task<TModel?> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<TModel?>(entity);
        }

        public async Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var couchEntities = await _repository.Get(entityPredicate);
            return _mapper.Map<IEnumerable<TModel>>(couchEntities);
        }

        public async Task Create(TModel item)
        {
            var couchEntity = _mapper.Map<TEntity>(item);
            await _repository.Create(couchEntity);
        }

        public async Task Update(TModel item)
        {
            var entity = _mapper.Map<TEntity>(item);
            await _repository.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
