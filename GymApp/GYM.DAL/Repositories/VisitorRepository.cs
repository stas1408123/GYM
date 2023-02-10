using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM.DAL.Repositories
{
    public class VisitorRepository : IRepository<VisitorEntity>
    {
        private readonly GymAppDbContext _context;
        public VisitorRepository(GymAppDbContext context)
        {
            _context = context;
        }
        public async Task Create(VisitorEntity item)
        {
            _context.VisitorEntities.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.VisitorEntities.FindAsync(id);
            if (item != null)
            {
                _context.VisitorEntities.Remove(item);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<VisitorEntity>> Find(Expression<Func<VisitorEntity, bool>> predicate)
        {
            return await _context.VisitorEntities.Where(predicate).ToListAsync();
        }
        public async Task<VisitorEntity?> Get(int id)
        {
            return await _context.VisitorEntities.FindAsync(id);
        }

        public async Task<IEnumerable<VisitorEntity>> GetAll()
        {
            return await _context.VisitorEntities.AsNoTracking().ToListAsync();
        }

        public async Task Update(VisitorEntity item)
        {
            _context.VisitorEntities.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
