using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string _tag;
        private FileInfo _saveFile;

        public InstagramScrapper(string tag)
        {
            _logger = new Logger(GetType(), LogOutput.Console);
            _tag = tag;
            _saveFile = new FileInfo(Path.Combine(Path.GetTempPath(), "imageUris.json"));
        }

        public async Task<Uri[]> GetImageUris()
        {
            var imageUris = new List<Uri>();

            var content = await _client.GetStringAsync(new Uri($"https://www.instagram.com/explore/tags/{_tag}/"));
            
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
                _logger.Log("Instagram did not provide us with a full page! Using FullInsta.Photo uris.");
                return await GetImagesFromFullInsta();
            }

            foreach (var edge in igResponse.EntryData.TagPage[0].Graphql.Hashtag.EdgeHashtagToMedia.Edges)
            {
                var imageUri = edge.Node.DisplayUrl;
                imageUris.Add(imageUri);
                _logger.Log($"Adding {imageUri}");
            }

            DumpImageUrisLocally(imageUris.ToArray());

            return imageUris.ToArray();
        }

        private async Task<Uri[]> GetImagesFromFullInsta()
        {
            var uris = new List<Uri>();

            var content = await _client.GetStringAsync(new Uri($"https://fullinsta.photo/hashtag/{_tag}/"));

            // TODO

            var aUris = uris.ToArray();

            if (aUris.Length > 0)
            {
                DumpImageUrisLocally(aUris);
                return aUris;
            }
            else
            {
                var text = await File.ReadAllTextAsync(_saveFile.FullName);
                var urisFromJson = JsonConvert.DeserializeObject<Uri[]>(text);
                if (urisFromJson.Length > 1)
                    return urisFromJson;
                else
                {
                    var exp = new ApplicationException("No data was able to be retrieved!");
                    _logger.Log(exp);
                    throw exp;
                }
            }
        }

        private void DumpImageUrisLocally(Uri[] imageUris)
        {
            var json = JsonConvert.SerializeObject(imageUris);
            File.AppendAllText(Path.Combine(_saveFile.FullName), json);
        }
    }
}
