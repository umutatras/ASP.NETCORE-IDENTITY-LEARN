using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Udemy.Identity.Entities;
using Udemy.Identity.Models;

namespace Udemy.Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = model.Email,
                    UserName = model.Username,
                    Gender = model.Gender

                };
                var identityResult=await _userManager.CreateAsync(user,model.Password);
                if(identityResult.Succeeded)
                {
                    await _roleManager.CreateAsync(new()
                    {
                        Name="Admin",
                        CreatedTime=DateTime.Now
                    });
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index");
                }
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn(string returnurl)
        {
            return View(new UserSignInModel(){ ReturnUrl=returnurl});
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if(ModelState.IsValid)
            {
                var user2 = await _userManager.FindByNameAsync(model.Username);
                var succesResult=await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);
                if(succesResult.Succeeded)
                {
                    if(!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                   
                    var roles=await _userManager.GetRolesAsync(user2);
                    if(roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("Panel");
                    }
                }
                else if (succesResult.IsLockedOut)
                {
                    var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user2);
                   
                    ModelState.AddModelError("", $"hesabınız {(lockoutEnd.Value.UtcDateTime-DateTime.UtcNow).Minutes} dakika askıya alındı");
                }
                else
                {
                    var message = string.Empty;
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user != null)
                    {
                        var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                        message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount)} kez daha girerseniz hesap geçici olarak kilitlenecektir.";
                    }
                    else
                    {
                        message = "Kullanıdı adı veya şifre hatalı";
                    }
                    ModelState.AddModelError("", message);

                }
             
           
                
            }

            return View(model);
        }
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var user = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
