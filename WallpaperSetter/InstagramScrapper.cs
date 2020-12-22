using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utilities;

namespace WallpaperSetter
{
    class InstagramScrapper
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client = new HttpClient();
        private readonly Uri _tagUri;

        public InstagramScrapper(string tag)
        {
            _logger = new Logger(GetType(), LogOutput.Console);
            _tagUri = new Uri($"https://www.instagram.com/explore/tags/{tag}/");
        }

        public async Task<Uri[]> GetImageUris()
        {
            var imageUris = new List<Uri>();

            var content = await _client.GetStringAsync(_tagUri);
            
            var aLinkRegex = new Regex("(<script type=\"text/javascript\">window._sharedData = ).*(</script>)");
            //var unicodeRegex = new Regex(@"\\u[0-9a-z]{4}");

            var parsedJson = aLinkRegex.Matches(content)[0].Value
                .Replace("<script type=\"text/javascript\">window._sharedData = ", "")
                .Replace(";</script>", "")
                .Replace(@"\u0026", "&")
                ;

            //parsedJson = unicodeRegex.Replace(parsedJson, "");

            var igResponse = JsonConvert.DeserializeObject<InstagramResponse>(parsedJson);

            // Rate Limited?
            if (igResponse.EntryData.TagPage is null)
            {
                var exp = new ApplicationException("Instagram did not provide us with a full page! Try again later...");
                _logger.Log(exp);
                throw exp;
            }

            foreach (var edge in igResponse.EntryData.TagPage[0].Graphql.Hashtag.EdgeHashtagToMedia.Edges)
            {
                var imageUri = edge.Node.DisplayUrl;
                imageUris.Add(imageUri);
                _logger.Log($"Adding {imageUri}");
            }

            return imageUris.ToArray();
        }
    }
}
