using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login(LoginInputModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // Dummy check: Replace this with your actual authentication logic
                if (model.Username == "admin" && model.Password == "password")
                {
                    // TODO: Add your authentication logic here

                    return View(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
        //    ViewData["Title"] = "Login";
        //    ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
