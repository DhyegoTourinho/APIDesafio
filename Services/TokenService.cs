using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIDesafio.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace APIDesafio.Services
{
    public class TokenService
    {
        public string Generate(Login login)
        {
            //Instancia do JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey); 
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(login),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            //Gera um Token
            var token = handler.CreateToken(tokenDescriptor);

            //Gera uma string do token
            return handler.WriteToken(token);
            
        }
    
        private static ClaimsIdentity GenerateClaims(Login login)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, login.UserName));
            return ci;
        }
    }
}
