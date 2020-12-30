using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WallpaperSetter.Library.Repositories
{
    public class InMemoryImageUriRepository : IImageUriRepository
    {
        private List<Uri> _imageUris = new List<Uri>();
        private string GetFileName(string imageTag)
        {
            return Path.Combine(Path.GetTempPath(), $"{imageTag}-images.json");
        }

        public IEnumerable<Uri> PopulateFromJsonFile(string imageTag)
        {
            try
            {
                var json = File.ReadAllText(GetFileName(imageTag));
                _imageUris = JsonConvert.DeserializeObject<List<Uri>>(json);
            }
            catch (Exception)
            {
                _imageUris = new List<Uri>();
            }

            return _imageUris;
        }

        public void StoreToJsonFile(string imageTag)
        {
            var json = JsonConvert.SerializeObject(_imageUris);
            File.WriteAllText(GetFileName(imageTag), json);
        }
        
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
        IEnumerable<Uri> PopulateFromJsonFile(string imageTag);
        void StoreToJsonFile(string imageTag);

        IEnumerable<Uri> GetAll();
        Uri Get(in int i);
        void Add(in Uri uri);
        void AddRange(in IEnumerable<Uri> uris);
        void RemoveAt(in int i);
    }
}