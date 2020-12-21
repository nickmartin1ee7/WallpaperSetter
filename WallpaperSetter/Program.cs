using System;

namespace WallpaperSetter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Wallpaper URI: ");
            var input = Console.ReadLine();

            try
            {
                var uri = new Uri(input);

                Wallpaper.Set(uri, Wallpaper.Style.Stretched);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
