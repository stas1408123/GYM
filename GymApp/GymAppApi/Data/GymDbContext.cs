using GYM.API.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GYM.API.Data
{
    public class GymDbContext:DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        }

        public DbSet<Couch> Couches { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Visitor> Visitors { get; set; } = null!;
    }
}
