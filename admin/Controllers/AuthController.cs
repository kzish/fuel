using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using Microsoft.AspNet.Identity.Owin;

namespace admin.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        fuelContext db = new fuelContext();

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            ViewBag.title = "Login";

            //seed the first user
            var users = db.Aspnetusers.ToList();
            if (users.Count == 0)
            {
                var admin_user = new IdentityUser
                {
                    UserName = "admin@fuel.com",
                    Email = "admin@fuel.com",
                    PasswordHash = "Rubiem#99"
                };
                var result = await userManager.CreateAsync(admin_user, admin_user.PasswordHash);
                //TempData["msg"] = result;
            }
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password, string ReturnUrl)
        {
            var id_user = new IdentityUser { UserName = email, Email = email };
            var result = await signInManager.PasswordSignInAsync(email, password,false, false);
            if (result.Succeeded)
            {
                
                TempData["msg"] = "result " + result;
                //return RedirectToAction("Dashboard", "Home");
                //signin as a company and redirect
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl) && ReturnUrl != "%2" && ReturnUrl != "/")
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            else
            {
                TempData["type"] = "error";
                TempData["msg"] = "Invalid credentials " + result;
                TempData["email"] = email;
                return View();
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("LogOff")]
        public async Task<ActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
