using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers
{
    public class ScoreHandler : IScoreHandler
    {
        public Task<List<Score>> Get100BestScores()
        {
            throw new NotImplementedException();
        }
    }
}
