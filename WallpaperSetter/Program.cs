using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using Serilog;
using WallpaperSetter.Library;
using WallpaperSetter.Library.Repositories;
using static System.Console;

namespace WallpaperSetter.Console
{
    internal class Program
    {
        private static int _timeInMilliseconds;
        private static string _tag;
        private static Wallpaper.Style _style;

        public static async Task Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger = logger;

            while (true)
            {
                Clear();
                ResetFields();

                try
                {
                    SetMinutesUntilNextWallpaper();
                    SetImageTag();
                    SetWallpaperStyle();
                    await RunConsoleApp();
                }
                catch (Exception e)
                {
                    logger.Error(e, "Something went wrong!");
                }

                Write("Press any key to continue...");
                ReadKey();
            }
        }

        private static void ResetFields()
        {
            _timeInMilliseconds = 0;
            _tag = string.Empty;
        }

        private static void SetMinutesUntilNextWallpaper()
        {
            Write("Minutes until next Wallpaper: ");
            var inputTimeInMinutes = ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(inputTimeInMinutes, out var timeInMinutes) || !IsPositive(timeInMinutes))
                throw new InvalidAmountOfTimeException($"{inputTimeInMinutes} is not a valid positive number!");

            SetTimeInMillisecondsFromMinutes(timeInMinutes);
        }

        private static void SetImageTag()
        {
            Write("Image Tag: ");
            _tag = ReadLine()?.Trim() ?? string.Empty;

            if (!IsPositive(_tag.Length))
                throw new InvalidImageTagException("Input image tag was empty!");
        }

        private static void SetWallpaperStyle()
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;

            Write("Wallpaper style (Tiled, Centered, Stretched): ");
            var inputStyle = textInfo.ToTitleCase(ReadLine()?.Trim() ?? string.Empty);

            if (!Enum.TryParse(inputStyle, out _style))
                throw new InvalidWallpaperStyleException($"{inputStyle} is not a valid wallpaper style!");
        }

        // TODO: Replace with a factory
        private static async Task RunConsoleApp()
        {
            var app = new ConsoleApp(
                Log.Logger,
                _timeInMilliseconds,
                _tag,
                _style);

            await app.Run();
        }

        private static bool IsPositive(int n)
        {
            return n > 0;
        }

        private static void SetTimeInMillisecondsFromMinutes(in int timeInMinutes)
        {
            _timeInMilliseconds = timeInMinutes * 1000 * 60;
        }
    }

    internal class InvalidAmountOfTimeException : ArgumentException
    {
        public InvalidAmountOfTimeException(string message)
            : base(message)
        {
        }
    }

    internal class InvalidImageTagException : ArgumentException
    {
        public InvalidImageTagException(string message)
            : base(message)
        {
        }
    }

    internal class InvalidWallpaperStyleException : ArgumentException
    {
        public InvalidWallpaperStyleException(string message)
            : base(message)
        {
        }
    }
}