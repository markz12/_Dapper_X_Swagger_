using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _Dapper_X_Swagger_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet(nameof(User))]
        public IActionResult User()
        {
            return Ok("Welcome to Dapper X Swagger API");
        }
    }
}