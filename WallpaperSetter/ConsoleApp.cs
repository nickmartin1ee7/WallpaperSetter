using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Utilities;
using WallpaperSetter.Library;
using WallpaperSetter.Library.Instagram;
using WallpaperSetter.Library.Repositories;
using static System.Console;
using Timer = System.Timers.Timer;

namespace WallpaperSetter.Console
{
    public class ConsoleApp
    {
        #region Constructor

        public ConsoleApp(int timeInterval, string igTag, Wallpaper.Style style)
        {
            _timer = new Timer(timeInterval);
            _igTag = igTag;
            _style = style;
            _logger = new Logger(GetType(), LogOutput.Console);
            _unitOfWork = UnitOfWorkFactory.Create();
        }

        #endregion


        #region Event Handlers

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var count = _unitOfWork.ImageUriRepository.GetAll().Count();

            if (_imageIndex >= count) _imageIndex = 0;

            var imageUri = _unitOfWork.ImageUriRepository.Get(_imageIndex);

            _imageIndex++;

            _logger.Log($"Updating wallpaper to ({_imageIndex}/{count}): {imageUri.AbsoluteUri}");

            Wallpaper.Set(imageUri, _style);
        }

        #endregion

        #region Methods

        public async Task Run()
        {
            _logger.Log("Starting core functionality");
            _timer.Elapsed += OnTimedEvent;

            var scrapper = new InstagramScrapper(_igTag);

            _logger.Log($"Scrapping Instagram for images with #{_igTag}");

            var uris = (await scrapper.GetImageUrisAsync()).ToList();

            _unitOfWork.ImageUriRepository.AddRange(uris);

            _logger.Log($"Populated with {uris.Count} images");

            RestartTimerAndInvokeHandler();

            _logger.Log($"Timer started for intervals of {_timer.Interval}ms");

            CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            while (true)
            {
                Write("Press enter to skip to next image...");
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

        #region Fields

        private static readonly ManualResetEvent _quitEvent = new ManualResetEvent(false);
        private readonly string _igTag;

        private readonly ILogger _logger;
        private readonly Timer _timer;
        private readonly Wallpaper.Style _style;
        private readonly IUnitOfWork _unitOfWork;

        private int _imageIndex;

        #endregion
    }
}