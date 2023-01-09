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

    private Dictionary<string, string> _animalImagesMock = new()
    {
        {
            "cat",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Calico_tabby_cat_-_Savannah.jpg/1200px-Calico_tabby_cat_-_Savannah.jpg"
        },
        {
            "tiger",
            "https://media.4-paws.org/5/4/4/c/544c2b2fd37541596134734c42bf77186f0df0ae/VIER%20PFOTEN_2017-10-20_164-3854x2667-1920x1329.jpg"
        },
        {
            "bear",
            "https://static01.nyt.com/images/2022/12/20/science/16tb-cinnamon-bear/16tb-cinnamon-bear-mobileMasterAt3x.jpg"
        },
        {
            "elephant",
            "https://sdzsafaripark.org/sites/default/files/teaser_image/elephants.jpg"
        },
        {
            "giraffe",
            "https://th-thumbnailer.cdn-si-edu.com/JSv1cLbd49hT1aAt_qOjyxEAXjM=/1000x750/filters:no_upscale()/https://tf-cmsv2-smithsonianmag-media.s3.amazonaws.com/filer/0c/5f/0c5fc6f8-1b9b-4510-8b15-163482a3e041/istock_6413768_medium.jpg"
        }
    };

    public async Task<string> GetPhotoUrl(string word)
    {
        //return _animalImagesMock[word];
        // Random random = new Random();
        // if(random.Next(1) == 1)
        // {
        return _blobStorageService.GenerateUrlForRandomImage(word);
        // }
        // return await _bingPhotoService.GetPhotoUrlFromBing(word);
    }
}