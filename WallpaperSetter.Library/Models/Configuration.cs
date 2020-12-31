namespace WallpaperSetter.Library.Models
{
    public class Configuration
    {
        public int InvervalInMinutes { get; set; } = 1;
        public string ImageTag { get; set; } = "dogs";
        public Wallpaper.Style Style { get; set; } = Wallpaper.Style.Stretched;
    }
}