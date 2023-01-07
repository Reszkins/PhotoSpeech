namespace PhotoSpeech.Services.Interfaces
{
    public interface IBingPhotoService
    {
        Task<string> GetPhotoUrlFromBing(string phrase);
    }
}
