using GYM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GYM.DAL.EF
{
    public class GymAppDbContext : DbContext
    {
        public DbSet<CouchEntity> CouchEntities { get; set; } = null!;
        public DbSet<OrderEntity> OrderEntities { get; set; } = null!;
        public DbSet<VisitorEntity> VisitorEntities { get; set; } = null!;

        public GymAppDbContext(DbContextOptions<GymAppDbContext> options) : base(options)
        {
        }
    }
}
