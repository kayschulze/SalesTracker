
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SalesTracker.Models;
using System.Threading.Tasks;
using SalesTracker.ViewModels;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesTracker.Controllers
{
    public class AccountController : Controller
    {

        private readonly SalesTrackerContext _db;
        private readonly UserManager<SalesAssociate> _userManager;
        private readonly SignInManager<SalesAssociate> _signInManager;

        public AccountController(UserManager<SalesAssociate> userManager, SignInManager<SalesAssociate> signInManager, SalesTrackerContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new SalesAssociate { UserName = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.Write("Login failed");
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = 
                await _signInManager.PasswordSignInAsync(model.Email,
                                                         model.Password, 
                                                         isPersistent: true, 
                                                         lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Item");
            }
            else
            {
                Console.WriteLine("Wrong Password Entered");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


    }
}
