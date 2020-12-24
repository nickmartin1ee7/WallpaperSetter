using System;
using System.Collections.Generic;
using System.Text;

namespace WallpaperSetter.Library
{
    public class ImageUriRepository : IImageUriRepository
    {
        private List<Uri> _imageUris = new List<Uri>();

        public Uri Get(in int imageIndex) => _imageUris[imageIndex];
        public IEnumerable<Uri> GetAll() => _imageUris;
        public void Add(Uri uri) => _imageUris.Add(uri);
        public void AddRange(IEnumerable<Uri> uris) => _imageUris.AddRange(uris);
    }

    public interface IImageUriRepository
    {
        IEnumerable<Uri> GetAll();
        void Add(Uri uri);
        void AddRange(IEnumerable<Uri> uris);
        Uri Get(in int imageIndex);
    }
}
