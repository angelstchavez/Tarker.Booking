using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tarker.Booking.Application.External.GetTokenJWT;

namespace Tarker.Booking.External.GetTokenJWT
{
    public class GetTokenJWTService : IGetTokenJWTService
    {
        private readonly IConfiguration configuration;

        private string SecretKey { get; }
        private string IssuerJWT { get; }
        private string AudienceJWT { get; }

        public GetTokenJWTService(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.SecretKey = configuration["Jwt:SecretKey"]?? string.Empty;
            this.IssuerJWT = configuration["Jwt:Issuer"] ?? string.Empty;
            this.AudienceJWT = configuration["Jwt:Audience"] ?? string.Empty;
        }

        public string Execute(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this.SecretKey);
            var signinKey = new SymmetricSecurityKey(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = this.IssuerJWT,
                Audience = this.AudienceJWT
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
