using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class VisitorRepository : IRepository<VisitorEntity>
    {
        private readonly GymAppDbContext _context;
        public VisitorRepository(GymAppDbContext context)
        {
            _context = context;
        }
        public void Create(VisitorEntity item)
        {
            _context.VisitorEntities.Add(item);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = _context.VisitorEntities.Find(id);
            if (item != null)
            {
                _context.VisitorEntities.Remove(item);
                return true;
            }
            return false;
        }

        public IEnumerable<VisitorEntity> Find(Func<VisitorEntity, bool> predicate)
        {
            return _context.VisitorEntities.Where(predicate); ;
        }

        public VisitorEntity? Get(int id)
        {
            return _context.VisitorEntities.Find(id);
        }

        public IEnumerable<VisitorEntity> GetAll()
        {
            return _context.VisitorEntities.ToList();
        }

        public void Update(VisitorEntity item)
        {
            _context.VisitorEntities.Update(item);
            _context.SaveChanges();
        }
    }
}
