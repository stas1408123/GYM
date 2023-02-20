namespace GYM.API.Extensions
{
    public static class CustomCors
    {
        private static readonly string DefaultName = "DefaultPolicy";

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy(DefaultName, builder => builder
                .WithOrigins("http://localhost:46409", "https://localhost:7163", "http://localhost:5227")
                .AllowAnyHeader()
                .AllowAnyMethod()
            ));
        }
    }
}
