using Core.DTOs.User;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountServicecs
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityUser> Get(string id)
        {
            var user=await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user!=null)
                await  _signInManager.SignInAsync(user,true);
            
        }

        public Task Logout()
        {//using _signInManager
            throw new NotImplementedException();
        }

        public Task Registre(RegisterDto registerDto)
        {
           // var user = await _userManager.CreateAsync();
            throw new NotImplementedException();
        }
    }
}
