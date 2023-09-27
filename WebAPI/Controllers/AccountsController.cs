using Core.DTOs.User;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
           _accountsService = accountsService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            
            return Ok(await _accountsService.Get(id));
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegisterDto user)
        {
            await _accountsService.Register(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        { 
            var response =await _accountsService.Login(user);
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountsService.Logout();
            return Ok();
        }
    }
}
