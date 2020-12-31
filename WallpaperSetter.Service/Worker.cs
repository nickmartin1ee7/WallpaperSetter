using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using WallpaperSetter.Library;
using WallpaperSetter.Service.Factories;
using WallpaperSetter.Library.Models;
using WallpaperSetter.Library.ImageUriProviders;
using System.Linq;
using WallpaperSetter.Library.Repositories;

namespace WallpaperSetter.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImageUriProvider _imageUriProvider;
        private readonly Configuration _configuration;
        private int _currentImageIndex;

        private readonly Subject<Unit> _wallpaperDownloadSubject = new Subject<Unit>();
        private IDisposable _downloadDisposable;
        private IDisposable _setWallpaperDisposable;


        public IObservable<long> intervalObservable { get; private set; }
        public IObservable<Unit> wallpaperDownloadObservable => _wallpaperDownloadSubject.AsObservable();

        public Worker(ILogger<Worker> logger, Configuration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _imageUriProvider = ImageUriProviderFactory.Create(_configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _imageUriProvider.RunAsync();

            intervalObservable = Observable.Interval(TimeSpan.FromMinutes(_configuration.InvervalInMinutes));

            _downloadDisposable = intervalObservable.Subscribe(DownloadWallpaper);
            _setWallpaperDisposable = wallpaperDownloadObservable.Subscribe(SetWallpaper);

            // Initialize first download.
            DownloadWallpaper(_currentImageIndex);
            
            /*
            if (stoppingToken.IsCancellationRequested)
            {
                _downloadDisposable.Dispose();
                _setWallpaperDisposable.Dispose();
            }
            */
        }

        private void DownloadWallpaper(long _)
        {
            _logger.LogInformation("Downloaded Image");
            
            _wallpaperDownloadSubject.OnNext(Unit.Default);
        }

        private void SetWallpaper(Unit _)
        {
            var imageUris = UnitOfWorkFactory.Create().ImageUriRepository.ImageUris.ToList();

            if (_currentImageIndex >= imageUris.Count)
                _currentImageIndex = 0;

            Wallpaper.Set(imageUris[_currentImageIndex++], _configuration.Style);

            _logger.LogInformation("Set Wallpaper");
        }
    }
}
