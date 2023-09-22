using Core.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            return Ok();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegisterDto user)
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
