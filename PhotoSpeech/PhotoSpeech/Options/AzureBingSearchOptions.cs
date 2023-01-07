namespace PhotoSpeech.Options
{
    public class AzureBingSearchOptions
    {
        public static string Section => "AzureBingSearch";

        public string Key { get; set; } = "";
        public string Endpoint { get; set; } = "";
    }
}
