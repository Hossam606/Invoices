using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task_Client.ViewModel;
using Task_DAL.Data;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbTaskContext _dbContext;
        private readonly IHomeRepository<User> _context;
        public AccountController(IHomeRepository<User> context, DbTaskContext dbContext)
        {
            _context = context;
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

                if ( _context.CheckUserInSignup(user))
                {
                    ViewBag.Notification = "This Account Has Already existed";
                    return View();
                }
                else
                {
                    await _context.Add(user);
                    ViewData["id"] = user.Id.ToString();
                    ViewData["user"] = user.Email.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            ViewData.Clear();
            return View();

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
