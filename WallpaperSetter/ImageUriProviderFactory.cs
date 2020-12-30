﻿using Serilog;
using WallpaperSetter.Library.ImageUriProviders;
using WallpaperSetter.Library.ImageUriProviders.Unsplash;
using WallpaperSetter.Library.Repositories;

namespace WallpaperSetter.Console
{
    internal static class ImageUriProviderFactory
    {
        internal static IImageUriProvider Create(string imageTag)
            => new UnsplashimageUriProvider(Log.Logger, UnitOfWorkFactory.Create(), imageTag);
    }
}
