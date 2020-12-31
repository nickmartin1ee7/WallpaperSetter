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
using WallpaperSetter.Library.Repositories;

namespace WallpaperSetter.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageUriProvider _imageUriProvider;
        private readonly Configuration _configuration;
        private int _currentImageIndex;
        
        private IDisposable _downloadDisposable;
        private IDisposable _setWallpaperDisposable;
        private Subject<Unit> _wallpaperDownloadSubject;

        public IObservable<long> intervalObservable { get; private set; }
        public IObservable<Unit> wallpaperDownloadObservable => _wallpaperDownloadSubject.AsObservable();

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, Configuration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _imageUriProvider = ImageUriProviderFactory.Create(_configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This is like a System.Timer, except it's more designed for reactive.
            intervalObservable = Observable.Interval(TimeSpan.FromMinutes(_configuration.InvervalInMinutes));

            _downloadDisposable = intervalObservable.Subscribe(downloadWallpaper);
            _setWallpaperDisposable = wallpaperDownloadObservable.Subscribe(setWallpaper);

            /*
            if (stoppingToken.IsCancellationRequested)
            {
                _downloadDisposable.Dispose();
                _setWallpaperDisposable.Dispose();
            }
            */

            await Task.CompletedTask;
        }

        private void downloadWallpaper(long _)
        {
            // Insert download logic here.
            _imageUriProvider.RunAsync().Wait();

            _wallpaperDownloadSubject.OnNext(Unit.Default);
        }

        private void setWallpaper(Unit _)
        {
            // Set Wallpaper here.
            Wallpaper.Set(_unitOfWork.ImageUriRepository.Get(_currentImageIndex++), _configuration.Style);
        }
    }
}
