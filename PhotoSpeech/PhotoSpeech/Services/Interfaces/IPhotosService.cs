namespace PhotoSpeech.Services.Interfaces;

public interface IPhotosService
{
    Task<string> GetPhotoUrl(string word);
}