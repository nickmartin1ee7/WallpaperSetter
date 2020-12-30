using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WallpaperSetter.Library.CustomExceptions;

namespace WallpaperSetter.Library.ImageUriProviders.Unsplash
{
    public class UnsplashimageUriProvider : IImageUriProvider
    {
        private readonly string _imgTag;

        private Uri TargetUri => new Uri($"https://unsplash.com/s/photos/{_imgTag}");

        public UnsplashimageUriProvider(string imgTag)
        {
            _imgTag = imgTag;
        }


        public async Task<IEnumerable<Uri>> RunAsync()
        {
            var responseObject = await GetUnsplashResposeJObject();

            var imageUris = responseObject["entities"]["photos"].Select(parent => new Uri(parent.First["urls"].First.First.Value<string>())).ToList();

            if (!imageUris.Any())
                throw new UnableToGetImageUrisException("Failed to parse images from Unsplash!");

            return imageUris;
        }

        private async Task<JObject> GetUnsplashResposeJObject()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(TargetUri);
            var regex = new Regex("(JSON.parse\\(\\\").*(\\\"\\);<\\/script>)");
            var strippedContentRegex = regex.Match(content);
            var json = strippedContentRegex.Value
                    .Replace("JSON.parse(\"", "")
                    .Replace("\");</script>", "")
                    .Replace("\\\"", "\"")
                ;
            var responseObject = JObject.Parse(json);
            return responseObject;
        }
    }
}