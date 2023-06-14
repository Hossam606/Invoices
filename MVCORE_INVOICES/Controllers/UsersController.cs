using DAL.Entities;
using DAL.ViewModels;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVCORE_INVOICES.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _context;
        public UsersController(IUserRepository context)
        {
            _context = context;
        }
        public IActionResult Index(string sterm = "")
        {
            if(sterm != null)
            {
                var users = _context.GetUsers(sterm);
                UserViewModel userModel = new UserViewModel
                {
                    Users = users,

                    STerm = sterm,

                };
                return View(userModel);
            }
            return View();
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel adduser)
        {
            if (ModelState.IsValid)
            {
                adduser.User.IsAdmin = _context.CheckIfAdminTo_SafeUserForFirstTime();
                await _context.Add(adduser.User);
                return RedirectToAction(nameof(Index));
            }
            return PartialView("AddPartialView", adduser);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UserViewModel userDisplayModel = new UserViewModel();
            var user = await _context.GetById(id);
            TempData["userID"] = user.Id;
            TempData.Keep();
            return new JsonResult(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User adduser)
        {
            if (ModelState.IsValid)
            {
                Int32 userId = (int)TempData["userID"];

                await _context.UpdateAsync(userId, adduser);
                return RedirectToAction(nameof(Index));
            }
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.User = adduser;
            return PartialView("UpdatePartialView", userViewModel);
        }
        [HttpGet]
        public  IActionResult Delete(int id)
        {
            var user = _context.GetById(id);
            if (user == null) return View("NotFound");
             _context.Delete(id);
            var users = _context.GetUsers("");
            UserViewModel userModel = new UserViewModel()
            {
                Users = users,
                STerm = ""
            };
            return View(nameof(Index), userModel);
            //return RedirectToAction(nameof(Index));
        }

    }
}
