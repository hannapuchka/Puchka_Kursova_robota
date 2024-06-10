using WebApp.Interfaces.Services;
using WebApp.Services;

namespace WebApp.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            return services;
        }
    }
}
