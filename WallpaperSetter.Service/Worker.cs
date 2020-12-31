using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using WallpaperSetter.Library;
using WallpaperSetter.Service.Factories;
using WallpaperSetter.Library.Models;
using WallpaperSetter.Library.ImageUriProviders;
using System.Linq;
using WallpaperSetter.Library.Repositories;
using System.IO;

namespace WallpaperSetter.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImageUriProvider _imageUriProvider;
        private readonly Configuration _configuration;
        private int _currentImageIndex;

        private IDisposable _setWallpaperDisposable;

        public IObservable<long> intervalObservable { get; private set; }
        

        public Worker(ILogger<Worker> logger, Configuration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var configJson = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            try
            {
                configJson.GetSection("Parameters").Bind(_configuration);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                 throw;
            }

            _imageUriProvider = ImageUriProviderFactory.Create(_configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Executing Service.");

            await _imageUriProvider.RunAsync();

            intervalObservable = Observable.Interval(TimeSpan.FromMinutes(_configuration.InvervalInMinutes));

            _setWallpaperDisposable = intervalObservable.Subscribe(SetWallpaper);

            SetWallpaper(0);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Service.");

            _setWallpaperDisposable.Dispose();

            return base.StopAsync(cancellationToken);
        }

        private void SetWallpaper(long _)
        {
            var imageUris = UnitOfWorkFactory.Create().ImageUriRepository.ImageUris.ToList();

            if (_currentImageIndex >= imageUris.Count)
                _currentImageIndex = 0;

            var currentWallpaper = imageUris[_currentImageIndex++];

            _logger.LogInformation($"Set Wallpaper to: {currentWallpaper}");

            Wallpaper.Set(currentWallpaper, _configuration.Style);
        }
    }
}
