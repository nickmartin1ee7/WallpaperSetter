using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WallpaperSetter.Library.Models;
using WallpaperSetter.Library.Repositories;

namespace WallpaperSetter.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<Configuration>();
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    // services.AddScoped<UnsplashImageUriProvider>();
                });
    }
}
