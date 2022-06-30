using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TFG.WebApi.Interfaces;
using TFG.WebApi.Models;

namespace TFG.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IRepository jwtManagerRepository;

        public UserController(IRepository jwtManagerRepository)
        {
            this.jwtManagerRepository = jwtManagerRepository;
        }

         
        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "John Smith",
                "Henry Good",
                "David Beckham"
            };

            return users;
        
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userData)
        {
            var token = jwtManagerRepository.Authenticate(userData);
            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
