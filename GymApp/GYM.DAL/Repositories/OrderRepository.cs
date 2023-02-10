using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM.DAL.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {
        private readonly GymAppDbContext _context;

        public OrderRepository(GymAppDbContext context)
        {
            _context = context;
        }

        public async Task Create(OrderEntity item)
        {
            _context.OrderEntities.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.OrderEntities.FindAsync(id);
            if (item != null)
            {
                _context.OrderEntities.Remove(item);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<OrderEntity>> Find(Expression<Func<OrderEntity, bool>> predicate)
        {
            return await _context.OrderEntities.Where(predicate).ToListAsync();
        }

        public async Task<OrderEntity?> Get(int id)
        {
            return await _context.OrderEntities.FindAsync(id);
        }

        public async Task<IEnumerable<OrderEntity>> GetAll()
        {
            return await _context.OrderEntities.AsNoTracking().ToListAsync();
        }

        public async Task Update(OrderEntity item)
        {
            _context.OrderEntities.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
