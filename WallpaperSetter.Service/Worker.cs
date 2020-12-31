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
using System.Collections.Generic;
using System.Linq;

namespace WallpaperSetter.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImageUriProvider _imageUriProvider;
        private readonly Configuration _configuration;
        private int _currentImageIndex;
        private List<Uri> _imageUris;

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
            _imageUris = (await _imageUriProvider.RunAsync()).ToList();

            intervalObservable = Observable.Interval(TimeSpan.FromMinutes(_configuration.InvervalInMinutes));

            _downloadDisposable = intervalObservable.Subscribe(downloadWallpaper);
            _setWallpaperDisposable = wallpaperDownloadObservable.Subscribe(setWallpaper);

            // Initialize first download.
            downloadWallpaper(_currentImageIndex);
            
            /*
            if (stoppingToken.IsCancellationRequested)
            {
                _downloadDisposable.Dispose();
                _setWallpaperDisposable.Dispose();
            }
            */
        }

        private void downloadWallpaper(long _)
        {
            _logger.LogInformation("Downloaded Image");
            
            _wallpaperDownloadSubject.OnNext(Unit.Default);
        }

        private void setWallpaper(Unit _)
        {
            Wallpaper.Set(_imageUris[_currentImageIndex++], _configuration.Style);

            _logger.LogInformation("Set Wallpaper");
        }
    }
}
