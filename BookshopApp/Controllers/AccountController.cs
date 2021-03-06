using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("Permission")]
        public IActionResult Permission()
        {
            return Ok(new { access = User.IsInRole("admin") });
        }

        [AllowAnonymous]
        [HttpPost("Check")]
        public IActionResult Check()
        {
            return Ok(new { username = User.FindFirstValue(ClaimTypes.Name) });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginQuery loginQuery)
        {
            var user = await _userManager.FindByEmailAsync(loginQuery.Login);
            if(user.Email == "admin@gmail.com")
            {
                var res = await _signInManager.PasswordSignInAsync(user, loginQuery.Password, true, false);
                if (res.Succeeded)
                {
                    return Ok();
                }
            }

            if(user == null)
            {
                return BadRequest(new[] { "Wrong email" });
            }
            //here we set and save cookie
            var result = await _signInManager.PasswordSignInAsync(user, loginQuery.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new[] { "Wrong password" });
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<ActionResult> Signup(UserSignUpQuery signupQuery)
        {
            var errors = new List<string>();

            var user = new User { UserName = signupQuery.UserName, Email = signupQuery.Email, DateOfRegistration = DateTime.Now };
            var result = await _userManager.CreateAsync(user, signupQuery.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return Ok();
            }

            errors.AddRange(result.Errors.Select(h=>h.Description));

            return BadRequest(errors.ToArray());
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            // delete cookie
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
