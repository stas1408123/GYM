using GYM.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM.API.Data
{
    public class GymDbContext : DbContext
    {
       
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<CouchViewModel> Couches { get; set; } = null!;
        public DbSet<OrderViewModel> Orders { get; set; } = null!;
        public DbSet<VisitorViewModel> Visitors { get; set; } = null!;
    }
}
