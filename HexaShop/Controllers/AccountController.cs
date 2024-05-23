using HexaShop.Core.Models;
using HexaShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVm memberLoginVm)
        {
            if (!ModelState.IsValid)
                return View();


            var user = await _userManager.FindByNameAsync(memberLoginVm.Username);

            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberLoginVm.Password, false, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }


        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVm)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(memberRegisterVm.Username);

            if(user != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            user = await _userManager.FindByEmailAsync(memberRegisterVm.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }

            user = new AppUser
            {
                UserName = memberRegisterVm.Username,
                FullName = memberRegisterVm.FullName,
                Email = memberRegisterVm.Email,
            };

            var result = await _userManager.CreateAsync(user, memberRegisterVm.Password);

            if(!result.Succeeded)
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Login", "Account");

        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
