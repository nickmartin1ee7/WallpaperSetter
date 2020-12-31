using System;

namespace WallpaperSetter.Library.CustomExceptions
{
    /// <summary>
    ///     Thrown by <see cref="ImageUriProviders"/> when they fail to provide a collection of Image URIs
    /// </summary>
    public sealed class UnableToGetImageUrisException : Exception
    {
        public UnableToGetImageUrisException(string message) : base(message)
        {
        }
    }
}