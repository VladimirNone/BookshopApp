using BookshopApp.Data;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
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

/*        [AllowAnonymous]
        public ActionResult Login(UserLoginQuery loginQuery)
        {
            
        }

        [AllowAnonymous]
        public ActionResult Signup(UserSignUpQuery signupQuery)
        {
            
        }

        public ActionResult Logout()
        {
        }*/
    }
}
