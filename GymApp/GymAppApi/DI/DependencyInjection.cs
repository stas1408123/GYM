using GYM.API.Mapping;
using GYM.BLL.Abstractions;
using GYM.BLL.DI;
using GYM.BLL.Models;
using GYM.BLL.Services;

namespace GYM.API.DI
{
    public static class DependencyInjection
    {
        public static void AddDependenciesApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApiMappingProfile));
            services.AddScoped<IService<OrderModel>, OrderService>();
            services.AddScoped<IService<VisitorModel>, VisitorService>();
            services.AddScoped<IService<CouchModel>, CouchService>();
            services.AddDependenciesBllLayer(configuration);
        }
    }
}
