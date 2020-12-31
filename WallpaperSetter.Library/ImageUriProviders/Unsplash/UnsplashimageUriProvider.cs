using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WallpaperSetter.Library.CustomExceptions;
using WallpaperSetter.Library.Repositories;

namespace WallpaperSetter.Library.ImageUriProviders.Unsplash
{
    public class UnsplashImageUriProvider : IImageUriProvider
    {
        #region Fields

        private readonly string _imageTag;
        private Uri TargetUri => new Uri($"https://unsplash.com/s/photos/{_imageTag}");

        #endregion
        
        #region Constructor

        public UnsplashImageUriProvider(string imageTag)
        {
            _imageTag = imageTag;
        }

        #endregion

        #region Public Methods

        public async Task RunAsync()
        {
            var unitOfWork = UnitOfWorkFactory.Create();
            var imageRepo = unitOfWork.ImageUriRepository;

            imageRepo.ImageUris = await TryGetImageUrisFromUnsplashResponseAsync();

            if (imageRepo.ImageUris is null)
            {
                imageRepo.PopulateFromJsonFile(_imageTag);
            }
            else
            {
                imageRepo.StoreToJsonFile(_imageTag);
            }

            if (!imageRepo.ImageUris.Any())
                throw new UnableToGetImageUrisException("Failed to get list of images from Unsplash and no previous results exist!");
        }

        #endregion

        #region Private Methods
        
        private async Task<IEnumerable<Uri>> TryGetImageUrisFromUnsplashResponseAsync()
        {
            using var client = new HttpClient();

            var content = await client.GetStringAsync(TargetUri);

            var regex = new Regex("(JSON.parse\\(\\\").*(\\\"\\);<\\/script>)");

            var strippedContentRegex = regex.Match(content);

            var json = strippedContentRegex.Value
                    .Replace("JSON.parse(\"", "")
                    .Replace("\");</script>", "")
                    .Replace("\\\"", "\"")
                    .Replace("\\\\\"", "\\\"")
                ;

            List<Uri> imageUris = null;

            try
            {
                var responseObject = JObject.Parse(json);
                imageUris = responseObject["entities"]["photos"]
                    .Select(parent => new Uri(parent.First["urls"].First.First.Value<string>())).ToList();

                // Hotfix: Unsplash initial image being a mountain
                imageUris.RemoveAt(0);
            }
            catch (JsonReaderException e)
            {
            }

            return imageUris;
        }

        #endregion

    }
}