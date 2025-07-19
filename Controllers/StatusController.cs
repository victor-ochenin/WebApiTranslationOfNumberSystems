using Microsoft.AspNetCore.Mvc;
using System;
using WebApiTranslationOfNumberSystems.Controllers;

namespace WebApiTranslationOfNumberSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet("")]
        public StringMessage GetStatus()
        {
            return new StringMessage(
                Message: "server is running", 
                Time: DateTime.UtcNow
            );
        }

        [HttpGet("/api/ping")]
        public StringMessage Ping()
        {
            return new StringMessage(
                Message: "pong",
                Time: DateTime.UtcNow
            );
        }
    }
} 