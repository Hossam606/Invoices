using Microsoft.AspNetCore.Mvc;

namespace MVCORE_INVOICES.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
