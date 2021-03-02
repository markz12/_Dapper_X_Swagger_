using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _Dapper_X_Swagger_.Services;
using Dapper;

namespace _Dapper_X_Swagger_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGlobalRepository _globalrepo;
        public UserController(IGlobalRepository globalrepo)
        {
            _globalrepo = globalrepo;
        }

        [HttpGet(nameof(User))]
        public IActionResult User()
        {
            return Ok("Welcome to Dapper X Swagger API");
        }
    }
}