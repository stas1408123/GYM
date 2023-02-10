using AutoMapper;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace GYM.BLL.Services
{
    public class OrderService : IGymService<OrderModel>
    {
        private readonly IRepository<OrderEntity> _repository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Create(OrderModel item)
        {
            var orderEntity = _mapper.Map<OrderEntity>(item);
            await _repository.Create(orderEntity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<OrderModel>> Find(Expression<Func<OrderModel, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<OrderEntity, bool>>>(predicate);
            var couchEntities = await _repository.Find(entityPredicate);
            return _mapper.Map<IEnumerable<OrderModel>>(couchEntities);
        }

        public async Task<OrderModel?> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<OrderModel?>(entity);
        }

        public async Task<IEnumerable<OrderModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<OrderEntity>, IEnumerable<OrderModel>>(entities);
        }

        public async Task Update(OrderModel item)
        {
            var entity = _mapper.Map<OrderEntity>(item);
            await _repository.Update(entity);
        }
    }
}
