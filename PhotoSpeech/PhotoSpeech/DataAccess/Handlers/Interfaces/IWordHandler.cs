using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface IWordHandler
    {
        Task<List<Word>> GetAllWords();
        Task<List<string>> GetAllWordsFromCategory(int categoryId);
    }
}
