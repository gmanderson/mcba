using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBCustomerSite.Data;
using IBCustomerSite.Models;
using IBCustomerSite.ViewModels;
using SimpleHashing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IBCustomerSite.Filters;

namespace IBCustomerSite.Controllers
{
    public class LoginController : Controller
    {
        private readonly MCBAContext _context;

        public LoginController(MCBAContext context) => _context = context;

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            var login = await _context.Logins.FindAsync(loginID);

            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { LoginID = loginID });
            }

            // If login is locked return customer to locked page
            if (login.IsLocked)
            {
                return RedirectToAction(nameof(Locked));
            }

            // Login customer.
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            return RedirectToAction("Index", "Customer");
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }

        public IActionResult Password() => RedirectToAction("Customer", "Details");

        public IActionResult PasswordChange()
        {
            int customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

            return View(new PasswordViewModel
            {
                CustomerID = customerID
            });
        }

        [AuthorizeCustomer]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordViewModel viewModel)
        {
            // Uses AsNoTracking so that db context can update db records. Otherwise complains about tracking another object already.
            Login login = await _context.Logins.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerID.Equals(viewModel.CustomerID));

            // Check old password is correct
            if(PBKDF2.Verify(login.PasswordHash, viewModel.OldPassword))
            {
                // Check for both new password fields match
                if(viewModel.RawPassword == viewModel.RawPasswordRepeat)
                {
                    login = login with { PasswordHash = PBKDF2.Hash(viewModel.RawPassword) };
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Customer");
                }

                // Returns to view if new passwords don't match
                ModelState.AddModelError(nameof(viewModel.RawPasswordRepeat), "Both new passwords don't match. Try again");
                return View(viewModel);

            }
            else
            {
                ModelState.AddModelError(nameof(viewModel.OldPassword), "Your password is incorrect");
                return View(viewModel);
            }

        }

        public IActionResult Locked()
        {
            return View();
        }
    }
}
