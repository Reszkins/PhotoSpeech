using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<User> GetUser(string login);
    }
}
