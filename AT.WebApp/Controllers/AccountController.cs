using AT.WebApp.Models.Account;
using AT.WebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AT.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private IAccountManager accountManager;

        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public IActionResult Login([FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] UserAccount userAccount, [FromQuery] string? returnUrl)
        {
            if (ModelState.IsValid == false)
            {
                return View(userAccount);
            }

            var result = this.accountManager.Login(userAccount.Email, userAccount.Senha).Result;

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl) == false)
                    return Redirect(returnUrl);

                return Redirect("/Home");
            }

            ViewBag.LoginError = "Email/Senha inválidos";

            return View(userAccount);
        }

        public IActionResult Logout()
        {
            this.accountManager.Logout();
            return RedirectToAction("Login");
        }
    }
}
