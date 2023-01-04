using Microsoft.AspNetCore.Identity;
using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;
using PhotoSpeech.Providers;

namespace PhotoSpeech.DataAccess.Handlers
{
    public class UserHandler : IUserHandler
    {
        private SqlDataAccess _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        private LoggedUserProvider _loggedUserProvider;
        public UserHandler(IPasswordHasher<User> passwordHasher, LoggedUserProvider loggedUserProvider)
        {
            _passwordHasher = passwordHasher;
            _db = new SqlDataAccess();
            _loggedUserProvider = loggedUserProvider;
        }
        public async Task<User?> GetUser(string login)
        {
            string sql = $"SELECT * FROM [dbo].[Users] WHERE [UserName] = '{login}'";

            var result = await _db.LoadData<User>(sql);
            if (result.Count < 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<bool> AddUser(User user)
        {
            string sql = $"INSERT INTO [dbo].[Users] (UserName, Password) VALUES ('{user.Username}', '{user.Password}')";

            await _db.SaveData(sql);
            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            var userDB = await GetUser(username);
            if (userDB == null)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(userDB, userDB.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            _loggedUserProvider.LogUserIn(username);

            return true;
        }

        public async Task<bool> Register(string username, string password)
        {
            var userDB = await GetUser(username);
            if (userDB != null)
            {
                return false;
            }

            User newUser = new User
            {
                Username = username,
            };
            var hashed = _passwordHasher.HashPassword(newUser, password);
            newUser.Password = hashed;

            var result = await AddUser(newUser);

            return result;
        }
    }
}
