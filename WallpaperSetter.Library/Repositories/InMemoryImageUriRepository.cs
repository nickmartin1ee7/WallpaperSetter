using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WallpaperSetter.Library.Repositories
{
    public interface IImageUriRepository
    {
        IEnumerable<Uri> ImageUris { get; set; }
        IEnumerable<Uri> PopulateFromJsonFile(string imageTag);
        void StoreToJsonFile(string imageTag);
    }

    public class InMemoryImageUriRepository : IImageUriRepository
    {
        public IEnumerable<Uri> ImageUris { get; set; } = new List<Uri>();

        private string GetFileName(string imageTag)
        {
            return Path.Combine(Path.GetTempPath(), $"{imageTag}-images.json");
        }

        public IEnumerable<Uri> PopulateFromJsonFile(string imageTag)
        {
            var fileName = new FileInfo(GetFileName(imageTag));

            try
            {
                var json = File.ReadAllText(fileName.FullName);
                ImageUris = JsonConvert.DeserializeObject<List<Uri>>(json);
            }
            catch (Exception)
            {
                if (fileName.Exists)
                {
                    fileName.Delete();
                }

                ImageUris = new List<Uri>();
            }

            return ImageUris;
        }

        public void StoreToJsonFile(string imageTag)
        {
            var json = JsonConvert.SerializeObject(ImageUris);
            File.WriteAllText(GetFileName(imageTag), json);
        }
    }
}