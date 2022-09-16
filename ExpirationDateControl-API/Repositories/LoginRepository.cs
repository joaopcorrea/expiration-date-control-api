using ExpirationDateControl_API.AuthorizationAndAuthentication;
using ExpirationDateControl_API.Repositories.Interfaces;

namespace ExpirationDateControl_API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _config;

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public Authenticate? Login(string username, string password)
        {
            var configLogin = _config["Authentication:login"];
            var configPassword = _config["Authentication:password"];

            if (configLogin.Equals(username) && configPassword.Equals(password))
                return new Authenticate() { Username = username, Password = password };
            else
                return null;
        }
    }
}
