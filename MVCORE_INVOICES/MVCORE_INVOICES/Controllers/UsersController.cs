using ClosedXML.Excel;
using DAL.Context;
using DAL.Entities;
using DAL.ViewModels;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.Drawing.Charts;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVCORE_INVOICES.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCORE_INVOICES.Controllers
{
    public class UsersController : Controller

    {
        private IWebHostEnvironment _environment;
        private readonly IConverter _converter;


        private readonly IUserRepository _context;
        public UsersController(IUserRepository context, IWebHostEnvironment environment, IConverter converter)
        {
            _context = context;
            _environment = environment;
            _converter = converter;
        }



        public IActionResult Index(string sterm = "")
        {
            var users = _context.GetUsers(sterm);
            UserViewModel userModel = new UserViewModel
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
            TempData["userIsAdmin"] = user.IsAdmin;

            return new JsonResult(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User adduser)
        {
            if (ModelState.IsValid)
            {
                Int32 userId = (int)TempData["userID"];
                adduser.Id = userId;
                adduser.IsAdmin = (bool)TempData["userIsAdmin"];
                await _context.UpdateAsync(userId, adduser);
                return RedirectToAction(nameof(Index));
            }
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.User = adduser;
            return PartialView("UpdatePartialView", userViewModel);
        }
        [HttpPost]
        public async  Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user =await  _context.GetById( int.Parse(id));
                if (user == null) return View("NotFound");
               await _context.Delete(int.Parse(id));
            }

            return Json(new { redirectToUrl = Url.Action("Index", "Users") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportToExcell()
        {
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
            try
            {

                var users = _context.GetUsers(null);
                UserViewModel userModel = new UserViewModel
                {
                    Users = users,

                    STerm = null

                };
                System.Data.DataTable dt = InvoicesHelper.ConvertToDataTable(userModel.Users.ToList());

                using (XLWorkbook xl = new XLWorkbook())
                {
                    xl.Worksheets.Add(dt, "Users");

                    using (MemoryStream mstream = new MemoryStream())
                    {
                        xl.SaveAs(mstream);
                        return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportToPdf()
        {
            string uploadpath = _environment.WebRootPath;
            string dest_path = Path.Combine(uploadpath, "PDF");

            if (!Directory.Exists(dest_path))
            {

                Directory.CreateDirectory(dest_path);
            }

            var globalSettings = new GlobalSettings
            {
                Orientation = DinkToPdf.Orientation.Portrait,
                PaperSize = PaperKind.A4,
                ColorMode = ColorMode.Color,
                Margins = new MarginSettings { Top = 20, Bottom = 10 },
                DocumentTitle = "Export to PDF",
                Out = dest_path + @"\Document.pdf"
            };
            var objects = new ObjectSettings()
            {
                HtmlContent = GetTablecontextHtml()
            };

            var PdfDoc = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objects }

            };

            _converter.Convert(PdfDoc);

            return new PhysicalFileResult(dest_path + @"\Document.pdf", "application/pdf");
        }

        public string GetTablecontextHtml()
        {
            var users = _context.GetUsers(null);
            UserViewModel userModel = new UserViewModel
            {
                Users = users,

                STerm = null

            };
            List<User> myUsers = new List<User>();
            myUsers = userModel.Users.ToList();

            var customerdata = new StringBuilder();

            customerdata.Append(@"<html>
                                  <head>
                                    </head>
                                  <body>
                                    <label>Users </label>                                 
                                 <table class='table-striped'>
                                        <thead>
                                            <tr>
                                            <th>User Name</th>
                                            <th>Full Name</th>
                                            <th>Email</th>
                                            <td>Phone</td>
                                             <td>IsAdmin</td>
                                            </tr>
 
                                        </thead>");

            foreach (var item in myUsers)
            {
                customerdata.AppendFormat(@"<tr>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td> 
                                            <td>{3}</td> 
                                        </tr>", item.UserName, item.FullName, item.Email, item.Phone, item.IsAdmin);
            }
            customerdata.Append(@"  
                                    </table> 
                                      </body>                                
                                      </html>");

            return customerdata.ToString();
        }



    }
}
