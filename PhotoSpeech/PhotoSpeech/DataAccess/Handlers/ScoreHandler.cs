using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;
using PhotoSpeech.Providers;

namespace PhotoSpeech.DataAccess.Handlers;

public class ScoreHandler : IScoreHandler
{
    private readonly LoggedUserProvider _loggedUserProvider;
    private readonly IUserHandler _userHandler;
    private readonly SqlDataAccess _db;

    public ScoreHandler(LoggedUserProvider loggedUserProvider, IUserHandler userHandler)
    {
        _db = new SqlDataAccess();
        _loggedUserProvider = loggedUserProvider;
        _userHandler = userHandler;
    }
    
    public async Task<List<Score>> Get100BestScores()
    {
        var sql = $"SELECT TOP 100 * FROM [dbo].[Scores] ORDER BY Value DESC";
        
        var scores = await _db.LoadData<Score>(sql);

        return scores;
    }

    public async Task SaveUserScore(int score)
    {
        // var username = _loggedUserProvider.GetUserName();
        // var user = await _userHandler.GetUser(username);
        //
        // var sql = $"INSERT INTO [dbo].[Scores] (UserId, Value) VALUES ('{user?.Id}', '{score}')";
        //
        // await _db.SaveData(sql);
    }
}