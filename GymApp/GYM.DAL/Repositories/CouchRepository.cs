using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM.DAL.Repositories
{
    public class CouchRepository : IRepository<CouchEntity>
    {
        private readonly GymAppDbContext _context;
        public CouchRepository(GymAppDbContext context)
        {
            _context = context;
        }

        public async Task Create(CouchEntity item)
        {
            _context.CouchEntities.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.CouchEntities.FindAsync(id);
            if (item != null)
            {
                _context.CouchEntities.Remove(item);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CouchEntity>> Find(Expression<Func<CouchEntity, bool>> predicate)
        {
            return await _context.CouchEntities.Where(predicate).ToListAsync();
        }

        public async Task<CouchEntity?> Get(int id)
        {
            return await _context.CouchEntities.FindAsync(id);
        }

        public async Task<IEnumerable<CouchEntity>> GetAll()
        {
            return await _context.CouchEntities.AsNoTracking().ToListAsync();
        }

        public async Task Update(CouchEntity item)
        {
            _context.CouchEntities.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
