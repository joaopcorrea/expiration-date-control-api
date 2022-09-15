using ExpirationDateControl_API.AuthorizationAndAuthentication;

namespace ExpirationDateControl_API.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Authenticate? Login(string username, string password);
    }
}
