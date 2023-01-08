using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface IScoreHandler
    {
        Task<List<Score>> Get100BestScores();
        Task SaveUserScore(int score);
    }
}
