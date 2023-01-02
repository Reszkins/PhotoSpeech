using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers
{
    public class UserHandler : IUserHandler
    {
        public Task<User> GetUser(string login)
        {
            throw new NotImplementedException();
        }
    }
}
