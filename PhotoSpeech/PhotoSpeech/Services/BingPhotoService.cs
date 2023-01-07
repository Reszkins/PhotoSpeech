using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using PhotoSpeech.Services.Interfaces;
using PhotoSpeech.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace PhotoSpeech.Services
{
    public class BingPhotoService : IBingPhotoService
    {
        private readonly AzureBingSearchOptions _azureBingSearchOptions;

        public BingPhotoService(IOptions<AzureBingSearchOptions> azureBingSearchOptions)
        {
            _azureBingSearchOptions = azureBingSearchOptions.Value;
        }
        public async Task<string> GetPhotoUrlFromBing(string phrase)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage();

            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(_azureBingSearchOptions.Endpoint + $"?q={phrase}");
            request.Headers.Add("Ocp-Apim-Subscription-Key", _azureBingSearchOptions.Key);

            HttpResponseMessage response = await client.SendAsync(request);
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var resultJObject = JObject.Parse(responseJson);
            var valuesProperty = resultJObject.Property("value");

            var values = resultJObject["value"];

            List<string> imageUrls = new();

            foreach(var value in values)
            {
                var imageUrl = value["contentUrl"];
                imageUrls.Add(imageUrl.ToString());
            }

            var random = new Random();
            var index = random.Next(imageUrls.Count);

            return imageUrls[index];
        }
    }
}
