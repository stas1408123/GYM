using GYM.BLL.Abstractions;
using GYM.BLL.DI;
using GYM.BLL.Models;
using GYM.BLL.Services;

namespace GYM.GrpcService.DI
{
    public static class DependencyInjection
    {
        public static void AddDependenciesGrpcApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenericService<OrderModel>, OrderService>();
            services.AddScoped<IGenericService<VisitorModel>, VisitorService>();
            services.AddScoped<IGenericService<CouchModel>, CouchService>();
            services.AddDependenciesBllLayer(configuration);
        }
    }
}
