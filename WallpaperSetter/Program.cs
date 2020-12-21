/*
 * TODO:
 *      Use events to fire at intervals to set wallpaper
 *      Automatically get wallpaper from an Instagram tag
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace WallpaperSetter
{
    class Program
    {
        private static string _igTag;
        private static Uri[] _imageUris;
        private static int _imageUriPos = 0;

        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static async Task Main(string[] args)
        {
            Console.Write("Minutes until next Wallpaper: ");
            var inputTime = Console.ReadLine()?.Trim();

            if (inputTime is null || inputTime is "") inputTime = "1";

            Console.Write("Instagram Tag: ");
            _igTag = Console.ReadLine()?.Trim() ?? string.Empty;

            int timeout = int.Parse(inputTime) * 1000 * 60;
            var t = new Timer(timeout);

            Console.WriteLine($"{DateTime.Now} - I'll see you at {DateTime.Now.AddMinutes(timeout)}");

            t.Start();
            t.Elapsed += OnTimedEvent;
            
            Console.CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            _quitEvent.WaitOne();

            Console.WriteLine("--PROCESS TERMINATED--");
            Console.ReadKey();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (_imageUris is null)
                PopulateImageUris().GetAwaiter().GetResult();

            // Get Uri of image
            var sNextImageUri = _imageUris[_imageUriPos];
            _imageUriPos++;

            Console.WriteLine($"{DateTime.Now} - Setting wallpaper to: {sNextImageUri.AbsoluteUri}");

            // Update wallpaper
            Wallpaper.Set(sNextImageUri, Wallpaper.Style.Stretched);
        }

        private static async Task PopulateImageUris()
        {
            var igScrapper = new InstagramScrapper(_igTag);
            Console.WriteLine("Populating images from hashtag...");
            _imageUris = await igScrapper.GetImageUris();
            Console.WriteLine("Done populating!");
        }
    }
}
