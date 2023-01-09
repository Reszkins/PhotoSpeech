namespace PhotoSpeech.Providers
{
    public class LoggedUserProvider
    {
        private bool _isUserLoggedIn = false;
        private string _username = "";

        public void LogUserIn(string username)
        {
            _isUserLoggedIn = true;
            _username = username;
        }

        public void LogUserOut() {
            _isUserLoggedIn = false;
            _username = "";
        } 

        public bool IsUserLoggedIn() => _isUserLoggedIn;

        public string GetUserName() => _username;
    }
}
