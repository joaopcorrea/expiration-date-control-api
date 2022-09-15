using ExpirationDateControl_API.AuthorizationAndAuthentication;
using ExpirationDateControl_API.Models;
using ExpirationDateControl_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpirationDateControl_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repository;
        private readonly GenerateToken _generateToken;

        public LoginController(ILoginRepository repository, GenerateToken generateToken)
        {
            _repository = repository;
            _generateToken = generateToken;
        }

        [HttpPost()]
        public IActionResult Login([FromBody] Authenticate authInfo)
        {
            var user = _repository.Login(authInfo.Username, authInfo.Password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha Inválidos" });

            var token = _generateToken.GenerateJwt(user.Username);
            user.Password = "";

            return Ok(new { user = user.Username, token });
        }
    }
}
