/*
 * TODO:
 *      Use events to fire at intervals to set wallpaper
 *      Automatically get wallpaper from an Instagram tag
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Utilities;
using WallpaperSetter.Library;
using Timer = System.Timers.Timer;

namespace WallpaperSetter.Console
{
    internal class Program
    {
        #region Fields

        private static ILogger _logger;
        private static string _igTag;
        private static Uri[] _imageUris;
        private static int _imageUriPos;

        #endregion

        #region Events

        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        
        #endregion

        public static async Task Main(string[] args)
        {
            _logger = new Logger(LogOutput.Console);

            System.Console.Write("Minutes until next Wallpaper: ");
            var inputTime = System.Console.ReadLine()?.Trim();

            if (inputTime is null || inputTime is "") inputTime = "1";

            System.Console.Write("Instagram Tag: ");
            _igTag = System.Console.ReadLine()?.Trim() ?? string.Empty;

            int timeout = int.Parse(inputTime) * 1000 * 60;
            var t = new Timer(timeout);
            
            t.Start();
            t.Elapsed += OnTimedEvent;
            
            System.Console.CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            await PopulateImageUris();

            _quitEvent.WaitOne();

            _logger.Log(LogLevel.Critical, "PROCESS TERMINATED");
        }

        private static async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (_imageUris is null || _imageUris.Length == 0)
                await PopulateImageUris();

            if (_imageUriPos < _imageUris.Length)
                _imageUriPos++;
            else
                _imageUriPos = 0;
            
            // Get Uri of image
            var sNextImageUri = _imageUris[_imageUriPos];
            
            _logger.Log($"Setting wallpaper to: {sNextImageUri.AbsoluteUri}");

            // Update wallpaper
            Wallpaper.Set(sNextImageUri, Wallpaper.Style.Stretched);
        }

        private static async Task PopulateImageUris()
        {
            var igScrapper = new InstagramScrapper(_igTag);
            _logger.Log($"Populating images from #{_igTag} ...");
            _imageUris = await igScrapper.GetImageUris();
            _logger.Log("Done populating!");
        }
    }
}
