using PhotoSpeech.Services.Interfaces;

namespace PhotoSpeech.Services;

public class PhotosService : IPhotosService
{
    private readonly IBingPhotoService _bingPhotoService;
    private readonly IBlobStorageService _blobStorageService;

    public PhotosService(IBingPhotoService bingPhotoService, IBlobStorageService blobStorageService)
    {
        _bingPhotoService= bingPhotoService;
        _blobStorageService= blobStorageService;
    }

    public async Task<string> GetPhotoUrl(string word)
    {
        Random random = new Random();
        if (random.Next(1) == 1)
        {
            return _blobStorageService.GenerateUrlForRandomImage(word);
        }
        return await _bingPhotoService.GetPhotoUrlFromBing(word);
    }
}