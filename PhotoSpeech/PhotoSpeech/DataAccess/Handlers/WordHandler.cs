using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers
{
    public class WordHandler : IWordHandler
    {
        public Task<List<Word>> GetAllWords()
        {
            throw new NotImplementedException();
        }

        public Task<List<Word>> GetAllWordsFrom(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
