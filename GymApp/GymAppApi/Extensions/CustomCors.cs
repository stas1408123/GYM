namespace GYM.API.Extensions
{
    public static class CustomCors
    {
        public const string DefaultCorsPolicy = "DefaultPolicy";

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy(DefaultCorsPolicy, builder => builder
                .WithOrigins("http://localhost:46409", "https://localhost:7163", "http://localhost:5227", "https://localhost:7079")
                .AllowAnyHeader()
                .AllowAnyMethod()
            ));
        }
    }
}
