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
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _imageTag;

        private Uri TargetUri => new Uri($"https://unsplash.com/s/photos/{_imageTag}");

        public UnsplashImageUriProvider(IUnitOfWork unitOfWork, string imageTag)
        {
            _unitOfWork = unitOfWork;
            _imageTag = imageTag;
        }

        public async Task<IEnumerable<Uri>> RunAsync()
        {
            var imageUris = await TryGetImageUrisFromUnsplashResponseAsync();

            if (imageUris is null)
            {
                imageUris = _unitOfWork.ImageUriRepository.PopulateFromJsonFile(_imageTag);
            }
            else
            {
                _unitOfWork.ImageUriRepository.AddRange(imageUris);
                _unitOfWork.ImageUriRepository.StoreToJsonFile(_imageTag);
            }

            if (!imageUris.Any())
                throw new UnableToGetImageUrisException("Failed to get list of images from Unsplash and no previous results exist!");

            return imageUris;
        }

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
    }
}