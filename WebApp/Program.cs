using WebApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddIdentityDefault();
            builder.Services.AddIdentityServices();
            builder.Services.AddServices();

            // Add services to the container.
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHttpsRedirection();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();
            
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Mainpage");
                await Task.CompletedTask;
            });

            app.MapRazorPages();
            app.Run();
        }
    }
}
