using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.JWT
{
    public class GenerateJWT
    {
        private IConfiguration _config;
       

        public GenerateJWT(IConfiguration config)
        {
            _config = config;
        }
        public string GetJWT(string email, string role)
        {
            var claims = new[] {
           
                    new Claim("Email", email),
                    new Claim("Role", role),
                   };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], role, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
