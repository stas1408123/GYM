using GYM.DAL.EF;
using GYM.DAL.Entities;
using GYM.DAL.Repositories;
using GYM.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GYM.DAL.DI
{
    public static class DependencyInjection
    {
        public static void AddDependenciesDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GymAppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRepository<CouchEntity>, CouchRepository>();
            services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
            services.AddScoped<IRepository<VisitorEntity>, VisitorRepository>();
        }
    }
}
