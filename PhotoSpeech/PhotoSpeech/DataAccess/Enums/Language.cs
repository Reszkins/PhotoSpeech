namespace PhotoSpeech.DataAccess.Enums;

public enum Language
{
    English = 0,
    Polish = 1,
    German = 2,
    Italian = 3,
    Spanish = 4,
    Ukrainian = 5
}

public static class LanguageExtensions
{
    public static string GetTranslateLanguageCode(this Language language)
    {
        return language switch
        {
            Language.English => "en",
            Language.Polish => "pl",
            Language.German => "de",
            Language.Italian => "it",
            Language.Spanish => "es",
            Language.Ukrainian => "uk",
            _ => "en"
        };
    }

    public static string GetSpeechToTextLanguageCode(this Language language)
    {
        return language switch
        {
            Language.English => "en-US",
            Language.Polish => "pl-PL",
            Language.German => "de-DE",
            Language.Italian => "it-IT",
            Language.Spanish => "es-ES",
            Language.Ukrainian => "uk-UA",
            _ => "en-US"
        };
    }
}