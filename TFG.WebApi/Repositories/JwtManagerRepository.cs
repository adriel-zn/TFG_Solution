
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TFG.WebApi.Interfaces;
using TFG.WebApi.Models;

namespace TFG.WebApi.Repositories
{
    public class JwtManagerRepository: IJwtManagerRepository
    {
        private readonly IConfiguration configuration;

        Dictionary<string, string> UsersList = new Dictionary<string, string>
        {
            {"Username1","Password1" },
            {"Username2","Password2" },
            {"Username3","Password3" },

        };

        public JwtManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(Users user)
        {
            if(!UsersList.Any(x => x.Key == user.Username && x.Value == user.Password))
            {
                return null;
            }

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { 
                new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenhandler.CreateToken(tokenDescription);

            return new Tokens { Token = tokenhandler.WriteToken(token) };

        }
    }
}
