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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CouchEntity>().HasData(
                new []{
                    new CouchEntity
                    {
                        Id = 1,
                        FirstName = "Mark",
                        LastName = "Walberg",
                        Description = "dsfasdfdv",
                        Visitors = new List<VisitorEntity>(),
                    },
                    new CouchEntity 
                    { 
                        Id = 2,
                        FirstName = "Nick",
                        LastName = "Walberg",
                        Description = "dsfasdfdv",
                        Visitors = new List<VisitorEntity>(),
                    }
                });
        }
    }
}
