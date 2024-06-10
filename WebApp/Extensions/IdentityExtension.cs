using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Extensions
{
    public static class IdentityExtension
    {
        private static string SymbolsUkranian = "абвгґдеєжзийіїклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИЙІЇКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        private static string SymbolsR = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private static string SymbolsEnglish = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string SymbolsSpecial = "0123456789-._@+";


        public static IServiceCollection AddIdentityDefault(this IServiceCollection services)
        {
            var allSymbols = SymbolsUkranian + SymbolsR + SymbolsEnglish + SymbolsSpecial;
            services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = allSymbols;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
            })
                .AddUserStore<UserStore<ApplicationUser, IdentityRole<Guid>, ProfileDbContext, Guid>>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationUser>>();

            return services;
        }
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.LoginPath = new PathString("/Login");
                o.Events = new CookieAuthenticationEvents()
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            }).AddCookie(IdentityConstants.ExternalScheme, o =>
            {
                o.Cookie.Name = IdentityConstants.ExternalScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            })
                .AddCookie(IdentityConstants.TwoFactorRememberMeScheme,
                    o => o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme)
                .AddCookie(IdentityConstants.TwoFactorUserIdScheme,
                    o =>
                    {
                        o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
                        o.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
                    }
                );
            services.AddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Login");
                });

            return services;
        }
    }
}
