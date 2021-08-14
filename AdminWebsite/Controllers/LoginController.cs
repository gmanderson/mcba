using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminWebsite.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _username = "admin";
        private readonly string _password = "admin";

        public LoginController()
        {

        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if((username != _username) && (password != _password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View();
            }

            // Login admin.
            HttpContext.Session.SetString("username", username);

            return RedirectToAction("Index", "Home");
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }
    }
}
