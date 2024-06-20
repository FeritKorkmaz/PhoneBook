using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.Models;

namespace PhoneBook.Services.TokenServices
{
    public class GenerateTokenService
    {
        private readonly IConfiguration _configuration;

        // Constructor: GenerateTokenService sınıfı oluşturulurken bir secret key alır
        public GenerateTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GenerateToken metodu: Kullanıcı adı ve password a göre bir token oluşturur
        public string GenerateToken(User user)
        {
            // Token handler oluşturulur
            var tokenHandler = new JwtSecurityTokenHandler();
            // Secret key byte dizisine dönüştürülür
            var key = Encoding.ASCII.GetBytes(_configuration["Token:SecurityKey"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Kullanıcı adı ve userId token içine claim olarak eklenir
                    user.UserName != null ? new Claim(ClaimTypes.Name, user.UserName) : new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                // Token'ın imzalanması için kullanılan kimlik bilgileri eklenir
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Token oluşturulur
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Token string olarak döndürülür
            return tokenHandler.WriteToken(token);
        }
    }
}
