using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QuickSocketDemo.Auth;
using QuickSocketDemo.Http;
using QuickSocketDemo.Settings;

namespace QuickSocketDemo 
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8080");
                        policy.WithMethods("*");
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IRepository, Repository>();
            builder.Services.AddSingleton<IQuickSocketSettings, QuickSocketSettings>();
            builder.Services.AddScoped<IQuickSocketCallbackVerifier, QuickSocketCallbackVerifier>();
            builder.Services.AddScoped<IQuickSocketApi, QuickSocketApi>();

            var app = builder.Build();

            app.UseAuthorization();          

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}