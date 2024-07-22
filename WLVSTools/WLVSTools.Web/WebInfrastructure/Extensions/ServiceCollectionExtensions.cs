using WLVSTools.Web.ApplicationServices;

namespace WLVSTools.Web.WebInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<GenerateApplicationViewService>();

            return services;
        }
    }
}
