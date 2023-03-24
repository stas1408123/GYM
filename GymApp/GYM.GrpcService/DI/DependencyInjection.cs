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
            //services.AddAutoMapper(typeof(ApiMappingProfile));
            services.AddScoped<IGenericService<OrderModel>, OrderService>();
            services.AddScoped<IGenericService<VisitorModel>, VisitorService>();
            services.AddScoped<IGenericService<CouchModel>, CouchService>();
            //services.AddValidatorsFromAssemblyContaining<CouchValidator>();
            services.AddDependenciesBllLayer(configuration);
        }
    }
}
