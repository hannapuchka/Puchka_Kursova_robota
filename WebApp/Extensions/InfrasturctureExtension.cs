using Microsoft.EntityFrameworkCore;
using WebApp.Context;

namespace WebApp.Extensions
{
    public static class InfrasturctureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ProfileDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));

            return services;
        }
    }
}
