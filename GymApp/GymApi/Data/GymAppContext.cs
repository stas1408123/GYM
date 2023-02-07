using GymApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GymApi.Data
{
    public class GymAppContex:DbContext
    {
        public DbSet<Couch> Couches { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GymApiDb;Trusted_Connection=True;");
        }
    }
}
