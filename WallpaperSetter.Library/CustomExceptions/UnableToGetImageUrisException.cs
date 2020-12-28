using System;
using System.Runtime.Serialization;

namespace WallpaperSetter.Library.CustomExceptions
{
    public class UnableToGetImageUrisException : Exception
    {
        public UnableToGetImageUrisException(string message) : base(message)
        {
            
        }
    }
}