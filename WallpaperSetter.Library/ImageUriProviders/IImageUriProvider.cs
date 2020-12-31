using System.Threading.Tasks;

namespace WallpaperSetter.Library.ImageUriProviders
{
    public interface IImageUriProvider
    {
        public Task RunAsync();
    }
}
