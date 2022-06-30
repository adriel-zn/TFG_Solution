
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
    public class Repository: IRepository
    {
        private readonly IConfiguration configuration;

        public List<Users> UsersList = new List<Users>
        {
            new Users()
            {
                Username = "Username1",
                Password = "Password1",
                Role = "Administrator"
            },
            new Users()
            {
                Username = "Username2",
                Password = "Password2",
                Role = "User"
            },
            new Users()
            {
                Username = "Username3",
                Password = "Password3",
                Role = "User"
            }

        };

        public Repository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(Users user)
        {
            if(!UsersList.Any(x => x.Username == user.Username && x.Password == user.Password))
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
