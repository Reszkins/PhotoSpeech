using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;
using PhotoSpeech.Providers;

namespace PhotoSpeech.DataAccess.Handlers;

public class ScoreHandler : IScoreHandler
{
    private readonly ISqlDataAccess _db;
    private readonly LoggedUserProvider _loggedUserProvider;
    private readonly IUserHandler _userHandler;

    public ScoreHandler(LoggedUserProvider loggedUserProvider, IUserHandler userHandler, ISqlDataAccess db)
    {
        _db = db;
        _loggedUserProvider = loggedUserProvider;
        _userHandler = userHandler;
    }

    public async Task<List<RankingVM>> Get100BestScores()
    {
        var sql = "SELECT TOP 100 * FROM [dbo].[Scores] ORDER BY Value DESC";

        var scores = await _db.LoadData<Score>(sql);
        var users = await _userHandler.GetAllUsers();

        var result = from user in users
            join score in scores
                on user.Id equals score.UserId
            orderby score.Value descending
            select new RankingVM {Name = user.Username, Score = score.Value};

        return result.ToList();
    }

    public async Task SaveUserScore(int score)
    {
        var username = _loggedUserProvider.GetUserName();
        var user = await _userHandler.GetUser(username);

        if (user is null) return;

        var sql = $"INSERT INTO [dbo].[Scores] (UserID, Value) VALUES ('{user.Id}', '{score}')";

        await _db.SaveData(sql);
    }
}