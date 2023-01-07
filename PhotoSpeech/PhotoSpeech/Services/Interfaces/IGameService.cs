using PhotoSpeech.DataAccess.Enums;

namespace PhotoSpeech.Services.Interfaces;

public interface IGameService
{
    Task<Dictionary<string, string>> GetPhotosWithLabels(List<string> words, int portion, Language language);
}