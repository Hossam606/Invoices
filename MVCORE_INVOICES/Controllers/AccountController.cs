using DAL.Context;
using DAL.Entities;
using DAL.ViewModels;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace MVCORE_INVOICES.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext_Invoices _dbContext;
        public AccountController(DBContext_Invoices dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(m => m.UserName == registerViewModel.UserName))
                {
                    ViewBag.Notification = "This Account Has Already existed";
                    return View();
                }
                else
                {
                    User user1 = new User();
                    user1.UserName = registerViewModel.UserName;
                    user1.Email = registerViewModel.Email;
                    user1.Password = registerViewModel.Password;
                    user1.Phone = registerViewModel.Phone;
                    user1.UserName = registerViewModel.UserName;
                    user1.IsAdmin = _dbContext.Users.ToList().Count>0?false:true;

                    await _dbContext.Users.AddAsync(user1);
                    await _dbContext.SaveChangesAsync();
                    ViewData["id"] = user1.Id.ToString();
                    ViewData["user"] = user1.Email.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut(LoginViewModel userModel)
        {
            HttpContext.Session.SetString("UserID", string.Empty);
            HttpContext.Session.SetString("UserName", string.Empty);
            HttpContext.Session.SetString("IsAdmin", string.Empty);

            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserID", string.Empty);
            HttpContext.Session.SetString("UserName", string.Empty);
            HttpContext.Session.SetString("IsAdmin", string.Empty);

            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public IActionResult login()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var checkLogin = _dbContext.Users.Where(x => x.Email.Equals(userModel.Email) && x.Password.Equals(userModel.Password)).FirstOrDefault();
                //var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
                if (checkLogin != null)
                {
                    HttpContext.Session.SetString("UserID", checkLogin.Id.ToString());
                    HttpContext.Session.SetString("UserName", checkLogin.UserName.ToString());
                    HttpContext.Session.SetString("IsAdmin", checkLogin.IsAdmin.ToString());

                    ViewData["Email"] = userModel.Email.ToString();
                    ViewData["Password"] = userModel.Password.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Notification = "Wrong UserName Or Password";
                }
                ModelState.AddModelError(string.Empty, "Invalid Email / Password");
            }
            return View();
        }
    }
}
