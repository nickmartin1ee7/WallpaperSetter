using System;
using System.Collections.Generic;

namespace WallpaperSetter.Library.Repositories
{
    public class ImageUriRepository : IImageUriRepository
    {
        private readonly List<Uri> _imageUris = new List<Uri>();

        public Uri Get(in int imageIndex)
        {
            return _imageUris[imageIndex];
        }

        public IEnumerable<Uri> GetAll()
        {
            return _imageUris;
        }

        public void Add(Uri uri)
        {
            _imageUris.Add(uri);
        }

        public void AddRange(IEnumerable<Uri> uris)
        {
            _imageUris.AddRange(uris);
        }
    }

    public interface IImageUriRepository
    {
        IEnumerable<Uri> GetAll();
        void Add(Uri uri);
        void AddRange(IEnumerable<Uri> uris);
        Uri Get(in int imageIndex);
    }
}