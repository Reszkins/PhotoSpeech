namespace PhotoSpeech.Services.Interfaces;

public interface ITranslatorService
{
    Task<string[]> TranslateWords(string[] words, string languageCode);
}