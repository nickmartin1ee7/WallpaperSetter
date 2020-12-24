using static System.Console;

namespace WallpaperSetter.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var timeInMinutes = 0;
            var tag = string.Empty;
            var valid = false;  // Sentry

            do
            {
                Clear();

                Write("Minutes until next Wallpaper: ");
                if (!int.TryParse(ReadLine()?.Trim(), out var inputTime)
                || inputTime <= 0)
                    continue;

                timeInMinutes = inputTime * 1000 * 60;

                Write("Instagram Tag: ");
                tag = ReadLine()?.Trim() ?? string.Empty;

                valid = tag.Length > 0;
            } while (!valid);

            var app = new ConsoleApp(timeInMinutes, tag);
            app.Run().GetAwaiter().GetResult();

            WriteLine("== PROGRAM ENDED ==");
            ReadKey();
        }
    }
}
