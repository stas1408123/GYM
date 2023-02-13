using GYM.DAL.EF;
using GYM.DAL.Entities;

namespace GYM.DAL.Repositories
{
    public class CouchRepository : GenericRepository<CouchEntity>
    {
        public CouchRepository(GymAppDbContext context) : base(context)
        {
        }
    }
}
