using GYM.DAL.EF;
using GYM.DAL.Entities;

namespace GYM.DAL.Repositories
{
    public class VisitorRepository : GenericRepository<VisitorEntity>
    {
        public VisitorRepository(GymAppDbContext context) : base(context)
        {
        }
    }
}
