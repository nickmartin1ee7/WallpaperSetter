using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Serilog;
using WallpaperSetter.Library;
using WallpaperSetter.Library.Repositories;
using static System.Console;
using Timer = System.Timers.Timer;

namespace WallpaperSetter.Console
{
    public class ConsoleApp
    {
        #region Fields

        private readonly string _imgTag;

        private readonly ILogger _logger;
        private readonly Timer _timer;
        private readonly Wallpaper.Style _style;

        private int _imageIndex;

        #endregion

        #region Constructor

        public ConsoleApp(ILogger logger, int timeInterval, string imgTag, Wallpaper.Style style)
        {
            _logger = logger;
            _timer = new Timer(timeInterval);
            _imgTag = imgTag;
            _style = style;
        }

        #endregion


        #region Event Handlers

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var unitOfWork = UnitOfWorkFactory.Create();
            var imageUris = unitOfWork.ImageUriRepository.ImageUris.ToList();
            
            if (_imageIndex >= imageUris.Count) _imageIndex = 0;

            var imageUri = imageUris[_imageIndex++];

            _logger.Information($"Updating wallpaper to ({_imageIndex}/{imageUris.Count}): {imageUri.AbsoluteUri}");

            Wallpaper.Set(imageUri, _style);
        }

        #endregion

        #region Methods

        public async Task Run()
        {
            _logger.Information("Starting core functionality");
            
            _timer.Elapsed += OnTimedEvent;

            var imageProvider = ImageUriProviderFactory.Create(_imgTag);

            _logger.Information($"Looking for images with #{_imgTag}");

            await imageProvider.RunAsync();
            
            _logger.Information($"Populated with {UnitOfWorkFactory.Create().ImageUriRepository.ImageUris.Count()} images");

            RestartTimerAndInvokeHandler();

            _logger.Information($"Timer started for intervals of {_timer.Interval}ms");
            
            while (true)
            {
                WriteLine("Press enter to skip to next image...");
                ReadKey();
                RestartTimerAndInvokeHandler();
            }
        }

        private void RestartTimerAndInvokeHandler()
        {
            _timer.Stop();
            OnTimedEvent(null, null); // Set wallpaper
            _timer.Start();
        }

        #endregion
    }
}