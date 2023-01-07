using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers;

public class WordHandler : IWordHandler
{
    private SqlDataAccess _db;

    public WordHandler()
    {
        _db = new SqlDataAccess();
    }
    
    public Task<List<Word>> GetAllWords()
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAllWordsFromCategory(int categoryId)
    {
        var sql = $"SELECT * FROM [dbo].[Words] WHERE [CategoryId] = '{categoryId}'";
        
        // var words = await _db.LoadData<Word>(sql);
        //
        // var wordValues = words.Select(w => w.Value).ToList();
        
        var wordValues = new List<string> {"cat", "tiger", "bear", "elephant", "giraffe"};
        
        return wordValues;
    }
}