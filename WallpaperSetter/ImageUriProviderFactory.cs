using WallpaperSetter.Library.ImageUriProviders;
using WallpaperSetter.Library.ImageUriProviders.Unsplash;

namespace WallpaperSetter.Console
{
    internal static class ImageUriProviderFactory
    {
        internal static IImageUriProvider Create(string imageTag)
            => new UnsplashimageUriProvider(imageTag);
    }
}
