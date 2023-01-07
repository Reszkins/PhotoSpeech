using PhotoSpeech.DataAccess.Enums;
using PhotoSpeech.Services.Interfaces;

namespace PhotoSpeech.Services;

public class GameService : IGameService
{
    private readonly ITranslatorService _translatorService;
    private readonly IPhotosService _photosService;
    private readonly Random _rand = new();

    public GameService(
        ITranslatorService translatorService,
        IPhotosService photosService)
    {
        _translatorService = translatorService;
        _photosService = photosService;
    }

    public async Task<Dictionary<string, string>> GetPhotosWithLabels(
        List<string> words,
        int portion,
        Language language)
    {
        var photosWithLabels = new Dictionary<string, string>();

        for (var i = 0; i < portion; i++)
        {
            if (!words.Any()) break;

            var randomIdx = _rand.Next(words.Count);
            var word = words[randomIdx];
            words.Remove(word);
            var photoUrl = await _photosService.GetPhotoUrl(word);
            photosWithLabels.Add(word, photoUrl);
        }
        
        var translatedWords = await _translatorService.TranslateWords(
            photosWithLabels.Keys.ToArray(),
            language.GetTranslateLanguageCode());

        var translatedPhotosWithLabels = new Dictionary<string, string>();

        for (var i = 0; i < photosWithLabels.Count; i++)
        {
            translatedPhotosWithLabels.Add(translatedWords[i], photosWithLabels.Values.ToArray()[i]);
        }

        return translatedPhotosWithLabels;
    }
}