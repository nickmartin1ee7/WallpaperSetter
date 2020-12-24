using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Utilities;
using WallpaperSetter.Library;
using Timer = System.Timers.Timer;
using static System.Console;

namespace WallpaperSetter.Console
{
    public class ConsoleApp
    {
        private readonly ILogger _logger;
        private readonly string _igTag;
        private readonly Timer _timer;
        private readonly IImageUriRepository _imageUriRepository;

        private int _imageIndex;

        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        public ConsoleApp(int timeInterval, string igTag)
        {
            _logger = new Logger(GetType(), LogOutput.Console);

            _igTag = igTag;
            _timer = new Timer(timeInterval);
            _imageUriRepository = new ImageUriRepository();
        }

        public async Task Run()
        {
            _logger.Log("Starting core functionality");
            _timer.Elapsed += OnTimedEvent;

            var scrapper = new InstagramScrapper(_igTag);

            _logger.Log($"Scrapping Instagram for images with #{_igTag}");

            var enumerableUris = await scrapper.GetImageUrisAsync();
            var uris = enumerableUris as Uri[] ?? enumerableUris.ToArray();

            _imageUriRepository.AddRange(uris);

            _logger.Log($"Populated with {uris.Length} images");

            if (uris.Length == 0)
            {
                var ex = new ApplicationException("No images were available. Try again later...");
                _logger.Log(ex);
                throw ex;
            }

            _timer.Start();
            _logger.Log($"Timer started for intervals of {_timer.Interval}ms");

            CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };
            
            _quitEvent.WaitOne();

            _timer.Elapsed -= OnTimedEvent;

            _logger.Log(LogLevel.Critical, "APPLICATION TERMINATED");
        }

        #region EventHandlers

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var imageUri = _imageUriRepository.Get(_imageIndex);

            _imageIndex++;

            _logger.Log($"Updating wallpaper to: {imageUri.AbsoluteUri}");

            Wallpaper.Set(imageUri, Wallpaper.Style.Stretched);
        }

        #endregion
    }
}
