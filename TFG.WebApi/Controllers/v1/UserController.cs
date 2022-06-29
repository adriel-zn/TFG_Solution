﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG.WebApi.Interfaces;
using TFG.WebApi.Models;

namespace TFG.WebApi.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IJwtManagerRepository jwtManagerRepository;

        public UserController(IJwtManagerRepository jwtManagerRepository)
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
