using ExpirationDateControl_API.AuthorizationAndAuthentication;
using ExpirationDateControl_API.Repositories.Interfaces;

namespace ExpirationDateControl_API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public Authenticate? Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
