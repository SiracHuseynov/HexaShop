using HexaShop.Core.Models;
using HexaShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole identityRole = new IdentityRole("SuperAdmin");
        //    IdentityRole identityRole1 = new IdentityRole("Admin");
        //    IdentityRole identityRole2 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(identityRole);
        //    await _roleManager.CreateAsync(identityRole1);
        //    await _roleManager.CreateAsync(identityRole2);

        //    return Ok("Rollar yarandi!");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        UserName = "Sirachh",
        //        FullName = "Sirac Huseynov"
        //    };

        //    await _userManager.CreateAsync(user, "Sirac123@");
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");

        //    return Ok("Admin yarandi!");
        //}

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(adminLoginVm.Username);

            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");

        }


        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
