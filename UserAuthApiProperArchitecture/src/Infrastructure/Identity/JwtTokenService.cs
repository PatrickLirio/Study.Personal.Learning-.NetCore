using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthApiProperArchitecture.Application.Interfaces;
using UserAuthApiProperArchitecture.Domain.Entities;

namespace UserAuthApiProperArchitecture.Infrastructure.Identity
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken (User user)
        {
            // Claims are statements about the user stored INSIDE the token 
            // Anyone who has the token can read these (so don't put secrets here!) 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                // Role claim is used by [Authorize(Roles="Admin")] attributes 
                new Claim(ClaimTypes.Role,           user.Role.ToString()),
                // JTI (JWT ID) makes each token unique 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //The secret key signs the token -only WE can produce valid tokens
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings: SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:ExpirationMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            // Serialize the token to a compact string: xxxxx.yyyyy.zzzzz HEADER.PAYLOAD.SIGNATURE
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
