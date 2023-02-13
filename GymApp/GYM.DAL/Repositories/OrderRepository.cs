using GYM.DAL.EF;
using GYM.DAL.Entities;

namespace GYM.DAL.Repositories
{
    public class OrderRepository : GenericRepository<OrderEntity>
    {
        public OrderRepository(GymAppDbContext context) : base(context)
        {
        }
    }
}
