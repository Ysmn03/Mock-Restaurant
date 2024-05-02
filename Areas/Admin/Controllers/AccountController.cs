using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restuarant.Areas.Admin.ViewModels;

namespace Restuarant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        // GET: AccountController
        public UserManager<IdentityUser> UserManager { get; set; }
        public SignInManager<IdentityUser>SignInManager { get; set; }
        public AccountController(SignInManager<IdentityUser>_SignInManager,
            UserManager<IdentityUser>_UserManager) 
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel collection)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Username or password is not correct");
                    return View();
                }
                var user = new IdentityUser
                {
                    Email=collection.Email,
                    UserName=collection.Email,
                };
                var Res = UserManager.CreateAsync(user, collection.Password).Result;
                if(Res.Succeeded)
                {
                    SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login","Account");
                }
                ModelState.AddModelError("", "Username or password is not correct");
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Username or password is not correct");
                    return View();
                }
                var Res = SignInManager.PasswordSignInAsync(collection.Email,
                    collection.Password, isPersistent: collection.RememberMe, false).Result;
                if(Res.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Username or password is not correct");
                return View();
            }
            catch
            {
                return View();
            }
        }
        //public ActionResult Logout()
        //{
        //    SignInManager.SignOutAsync();
        //    return RedirectToAction("Login", "Account");
        //}
    }
}
