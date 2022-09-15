using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpirationDateControl_API.AuthorizationAndAuthentication
{
    public class GenerateToken
    {
        private readonly TokenConfiguration _configuration;
        public GenerateToken(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwt(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var nameClaim = new Claim(ClaimTypes.Name, username);
            List<Claim> claims = new();
            claims.Add(nameClaim);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_configuration.ExpirationtimeInHours),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
