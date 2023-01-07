namespace PhotoSpeech.Options;

public class AzureCognitiveOptions
{
    public static string Section => "AzureCognitive";

    public string Key { get; set; } = "";
    public string Endpoint { get; set; } = "";
    public string Location { get; set; } = "";
}