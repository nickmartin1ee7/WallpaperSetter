using WallpaperSetter.Library.ImageUriProviders;
using WallpaperSetter.Library.ImageUriProviders.Unsplash;
using WallpaperSetter.Library.Models;

namespace WallpaperSetter.Service.Factories
{
    internal static class ImageUriProviderFactory
    {
        internal static IImageUriProvider Create(Configuration configuration)
            => new UnsplashImageUriProvider(configuration.ImageTag);
    }
}