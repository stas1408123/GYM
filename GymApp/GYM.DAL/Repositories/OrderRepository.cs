using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {
        private readonly GymAppDbContext _context;

        public OrderRepository(GymAppDbContext context)
        {
            _context = context;
        }
        public void Create(OrderEntity item)
        {
            _context.OrderEntities.Add(item);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = _context.OrderEntities.Find(id);
            if (item != null)
            {
                _context.OrderEntities.Remove(item);
                return true;
            }
            return false;
        }

        public IEnumerable<OrderEntity> Find(Func<OrderEntity, bool> predicate)
        {
            return _context.OrderEntities.Where(predicate);
        }

        public OrderEntity? Get(int id)
        {
            return _context.OrderEntities.Find(id);
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            return _context.OrderEntities.ToList();
        }

        public void Update(OrderEntity item)
        {
            _context.OrderEntities.Update(item);
            _context.SaveChanges();
        }
    }
}
