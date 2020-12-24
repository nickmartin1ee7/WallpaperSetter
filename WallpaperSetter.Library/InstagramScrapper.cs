using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utilities;

namespace WallpaperSetter.Library
{
    public class InstagramScrapper
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client = new HttpClient();
        private readonly string _tag;
        private FileInfo _saveFile;

        public InstagramScrapper(string tag)
        {
            _logger = new Logger(GetType(), LogOutput.Console);
            _tag = tag;
            _saveFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"{tag}-imageUris.json"));
        }

        public async Task<IEnumerable<Uri>> GetImageUrisAsync()
        {
            var imageUris = await GetImageUrisFromInstagramAsync();
            imageUris ??= await GetImagesFromFullInstaAsync();
            imageUris ??= await GetImagesFromPreviousResults();

            return imageUris;
        }

        private async Task<IEnumerable<Uri>> GetImageUrisFromInstagramAsync()
        {
            var content = await _client.GetStringAsync(new Uri($"https://www.instagram.com/explore/tags/{_tag}/"));

            var aLinkRegex = new Regex("(<script type=\"text/javascript\">window._sharedData = ).*(</script>)");

            var parsedJson = aLinkRegex.Matches(content)[0].Value
                    .Replace("<script type=\"text/javascript\">window._sharedData = ", "")
                    .Replace(";</script>", "")
                    .Replace(@"\u0026", "&")
                ;

            var igResponse = JsonConvert.DeserializeObject<InstagramResponse>(parsedJson);

            // Rate Limited?
            if (igResponse.EntryData.TagPage is null)
            {
                return null;
            }

            var imageUris = igResponse.EntryData.TagPage[0].Graphql.Hashtag.EdgeHashtagToMedia.Edges
                .Select(edge => edge.Node.DisplayUrl).ToList();

            DumpImageUrisLocally(imageUris);

            _logger.Log("Populated images from Instagram");

            return imageUris;
        }

        private async Task<IEnumerable<Uri>> GetImagesFromFullInstaAsync()
        {
            var uris = new List<Uri>();

            var content = await _client.GetStringAsync(new Uri($"https://fullinsta.photo/hashtag/{_tag}/"));

            // TODO

            var aUris = uris.ToArray();

            if (aUris.Length == 0) return null;

            DumpImageUrisLocally(aUris);

            _logger.Log("Populated images from FullInsta");

            return aUris;

        }

        private async Task<IEnumerable<Uri>> GetImagesFromPreviousResults()
        {
            // Read text
            var text = await File.ReadAllTextAsync(_saveFile.FullName);

            // Deserialize
            var urisFromJson = JsonConvert.DeserializeObject<Uri[]>(text);

            if (urisFromJson?.Length == 0)
                return null;

            _logger.Log("Populated images from previous results");

            return urisFromJson;
        }

        private void DumpImageUrisLocally(IEnumerable<Uri> imageUris)
        {
            var json = JsonConvert.SerializeObject(imageUris);
            File.WriteAllText(Path.Combine(_saveFile.FullName), json);
        }
    }
}
