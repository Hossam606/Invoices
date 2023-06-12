using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Client.ViewModel;
using Task_DAL.Data;
using Task_Entities.Entities;
namespace Task_Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbTaskContext _dbContext;
        public AccountController(DbTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(m => m.UserName == user.UserName))
                {
                    ViewBag.Notification = "This Account Has Already existed";
                    return View();
                }
                else
                {
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    ViewData["id"] = user.Id.ToString();
                    ViewData["user"] = user.Email.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
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
                var checkLogin=_dbContext.Users.Where(x=>x.Email.Equals(userModel.Email)&& x.Password.Equals(userModel.Password)).FirstOrDefault();
                //var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
                if (checkLogin !=null)
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
