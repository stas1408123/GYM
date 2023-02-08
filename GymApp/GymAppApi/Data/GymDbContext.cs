using GymAppApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GYM.API.Data
{
    public class GymDbContext:DbContext
    {
       
        public DbSet<Couch> Couches { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Visitor> Visitors { get; set; } = null!;

    }
}
