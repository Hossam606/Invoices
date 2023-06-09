using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Task_Client.Models;
using Task_DAL.Data;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class UserController : Controller
    {
         
        private readonly IHomeRepository<User> _context;
        public UserController(IHomeRepository<User> context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(string sterm = "")
        {
            var users = await _context.GetUsers(sterm);
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
                //await _db.Users.AddAsync(adduser);
                //_db.SaveChanges();
                await _context.Add(adduser);
                return RedirectToAction(nameof(Index));
                 
            }   
            return View(adduser); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var user = await _db.Users.FindAsync(id);
            //if (user == null) return View("NotFound");
            //return View(user);
            var x =await _context.GetById(id);
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,User user)
        {
            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.GetById(id);
            if (user == null) return View("NotFound");
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var user = await _context.GetById(id);
            if (user == null) return View("NotFound");
            await _context.Delete(id);    
            return RedirectToAction(nameof(Index));

        }

        //public ActionResult ExportToPDF()
        //{
        //    // Retrieve the contents of the grid
        //    var gridContents = GetGridContents();
        //    // Export the grid contents to a PDF file
        //    ExportToPDF(gridContents); // or ExportToExcel(gridContents);
        //                               // Provide feedback to the user
        //    TempData["Message"] = "File exported successfully.";
        //    return RedirectToAction("Index");
        //}
        //public void ExportToPDF(List<List<string>> gridContents)
        //{
        //    var document = new Document();
        //    PdfWriter.GetInstance(document, new FileStream("grid.pdf", FileMode.Create));
        //    document.Open();
        //    var table = new PdfPTable(gridContents[0].Count);
        //    foreach (var row in gridContents)
        //    {
        //        foreach (var cell in row)
        //        {
        //            table.AddCell(cell);
        //        }
        //    }
        //    document.Add(table);
        //    document.Close();
        //}

    }
}
