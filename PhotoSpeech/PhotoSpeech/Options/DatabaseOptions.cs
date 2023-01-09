namespace PhotoSpeech.Options;

public class DatabaseOptions
{
    public static string Section => "DatabaseStrings";

    public string PhotoSpeachDb { get; set; } = "";

}