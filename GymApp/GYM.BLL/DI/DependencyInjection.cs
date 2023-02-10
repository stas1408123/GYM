using GYM.BLL.Mapping;
using GYM.DAL.DI;
using GYM.DAL.Entities;
using GYM.DAL.Repositories;
using GYM.DAL.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GYM.BLL.DI
{
    public static class DependencyInjection
    {
        public static void AddDependenciesBllLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CouchModelMapping), typeof(OrderModelMapping), typeof(VisitorModelMapping));
            services.AddScoped<IRepository<CouchEntity>, CouchRepository>();
            services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
            services.AddScoped<IRepository<VisitorEntity>, VisitorRepository>();
            services.AddDependenciesDataAccessLayer(configuration);
        }

    }
}
