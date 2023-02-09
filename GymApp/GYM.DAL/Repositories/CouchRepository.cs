using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories.Abstractions;

namespace GYM.DAL.Repositories
{
    public class CouchRepository : IRepository<CouchEntity>
    {
        private readonly GymAppDbContext _context;
        public CouchRepository(GymAppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<CouchEntity> GetAll()
        {
            return _context.CouchEntities.ToList();
        }

        public CouchEntity? Get(int id)
        {
            return _context.CouchEntities.Find(id);
        }

        public IEnumerable<CouchEntity> Find(Func<CouchEntity, bool> predicate)
        {
            return _context.CouchEntities.Where(predicate);
        }

        public void Create(CouchEntity item)
        {
            _context.CouchEntities.Add(item);
            _context.SaveChanges();
        }

        public void Update(CouchEntity item)
        {
            _context.CouchEntities.Update(item);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = _context.CouchEntities.Find(id);
            if (item != null)
            {
                _context.CouchEntities.Remove(item);
                return true;
            }
            return false;
        }
    }
}
