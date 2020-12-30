using System;
using System.Collections.Generic;

namespace WallpaperSetter.Library.Repositories
{
    public class ImageUriRepository : IImageUriRepository
    {
        private readonly List<Uri> _imageUris = new List<Uri>();

        public Uri Get(in int i)
        {
            return _imageUris[i];
        }

        public IEnumerable<Uri> GetAll()
        {
            return _imageUris;
        }

        public void Add(in Uri uri)
        {
            _imageUris.Add(uri);
        }

        public void AddRange(in IEnumerable<Uri> uris)
        {
            _imageUris.AddRange(uris);
        }

        public void RemoveAt(in int i)
        {
            _imageUris.RemoveAt(i);
        }
    }

    public interface IImageUriRepository
    {
        IEnumerable<Uri> GetAll();
        Uri Get(in int i);
        void Add(in Uri uri);
        void AddRange(in IEnumerable<Uri> uris);
        void RemoveAt(in int i);
    }
}