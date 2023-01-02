using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface IWordHandler
    {
        Task<List<Word>> GetAllWords();
        Task<List<Word>> GetAllWordsFrom(Category category);
    }
}
