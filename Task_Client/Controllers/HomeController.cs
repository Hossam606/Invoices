using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task_Client.Models;
using Task_DAL.Data;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbTaskContext _db;
        private readonly IHomeRepository _context;
        public HomeController(ILogger<HomeController> logger, DbTaskContext db, IHomeRepository context)
        {
            _logger = logger;
            _db = db;
            _context = context;
        }

        public IActionResult Index()
        {
             
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
     
    
}
