using Ecommerce.Data;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{

    public class AccountController : Controller
    {

        private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper)
        {
            this._userHelper = userHelper;
        }
        public IActionResult Login()
        {

            //Valido si en caso de estar autenticado me mande al home
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        //deslogueó

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotAuthorized()
        {
           
            return View();
        }



    }
}
