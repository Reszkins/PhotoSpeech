using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<User?> GetUser(string login);
        Task<bool> AddUser(User user);
        Task<bool> Login(string username, string password);
        Task<bool> Register(string username, string password);
        Task<List<User>> GetAllUsers();
    }
}
