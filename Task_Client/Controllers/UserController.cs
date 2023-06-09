using Microsoft.AspNetCore.Mvc;
using Task_Client.Models;
using Task_DAL.Data;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _user;
        private readonly DbTaskContext _db;
        private readonly IHomeRepository _context;
        public UserController(IRepository<User> user, DbTaskContext db, IHomeRepository context)
        {
            _user = user;
            _db = db;
            _context = context;
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    //var all = _usercontext.GetAll();
        //    //return View(all);
        //    return View(_db.Users.ToList());
        //}

        //[HttpPost]
        public async Task<IActionResult> Index(string sterm = "")
        {
            var users = await _context.GetBooks(sterm);
            UserDisplayModel userModel = new UserDisplayModel
            {
                Users = users,

                STerm = sterm,

            };
            return View(userModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User adduser)
        {
            if (ModelState.IsValid)
            {
                await _db.Users.AddAsync(adduser);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
                 
            }   
            return View(adduser); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return View("NotFound");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,User user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return View("NotFound");
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return View("NotFound");
            await _db.Users.FindAsync(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}
