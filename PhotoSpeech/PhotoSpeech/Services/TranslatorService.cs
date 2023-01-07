using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhotoSpeech.Options;
using PhotoSpeech.Services.Interfaces;

namespace PhotoSpeech.Services;

public class TranslatorService : ITranslatorService
{
    private readonly AzureCognitiveOptions _azureCognitiveOptions;

    public TranslatorService(IOptions<AzureCognitiveOptions> azureCognitiveOptions)
    {
        _azureCognitiveOptions = azureCognitiveOptions.Value;
    }

    public async Task<string[]> TranslateWords(string[] words, string languageCode)
    {
        var route = $"/translate?api-version=3.0&from=en&to={languageCode}";
        var body = new object[] {new {Text = string.Join(';', words)}};
        var requestBody = JsonConvert.SerializeObject(body);

        using var client = new HttpClient();
        using var request = new HttpRequestMessage();

        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(_azureCognitiveOptions.Endpoint + route);
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        request.Headers.Add("Ocp-Apim-Subscription-Key", _azureCognitiveOptions.Key);

        // location required if you're using a multi-service or regional (not global) resource.
        request.Headers.Add("Ocp-Apim-Subscription-Region", _azureCognitiveOptions.Location);

        HttpResponseMessage response = await client.SendAsync(request);

        var resultJson = await response.Content.ReadAsStringAsync();
        var resultJArray = JArray.Parse(resultJson);
        var result = resultJArray[0]["translations"]?[0]?["text"]?.ToString()!;
        var resultArray = result.Split(';');

        return resultArray;
    }
}