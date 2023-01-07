namespace PhotoSpeech.Services.Interfaces
{
    public interface IBlobStorageService
    {
        string GenerateUrlForRandomImage(string nameOfTheItem);
        Task SaveFileToBlobStorage(FileStream file, string nameOfTheItem);
    }
}
