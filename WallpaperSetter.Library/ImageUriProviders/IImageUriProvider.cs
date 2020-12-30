using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WallpaperSetter.Library.ImageUriProviders
{
    public interface IImageUriProvider
    {
        public Task<IEnumerable<Uri>> RunAsync();
    }
}
