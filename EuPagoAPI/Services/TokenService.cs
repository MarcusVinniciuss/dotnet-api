using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EuPagoAPI.Services
{
    public static class TokenService
    {

        public static JwtSecurityToken GenerateToken(long CPF, IConfiguration configuration)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(CPF))
                };

            foreach (var audience in configuration.GetSection("Jwt:Audiences").Get<string[]>())
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            var token = new JwtSecurityToken
            (
                issuer: configuration.GetSection("Jwt:Issuer").Get<string>(),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Get<string>())),
                    SecurityAlgorithms.HmacSha256
                    )
            );

            return token;
        }

    }
}
