namespace WallpaperSetter.Library.Models
{
    public record Configuration
    {
        public int InvervalInMinutes { get; set; }
        public string ImageTag { get; set; }
        public Wallpaper.Style Style { get; set; }
    }
}