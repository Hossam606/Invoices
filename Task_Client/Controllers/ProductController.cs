using Microsoft.AspNetCore.Mvc;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHomeRepository<Product> _context;
        public ProductController(IHomeRepository<Product> context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allProduct = _context.GetAll();
            return View(allProduct);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product addproduct)
        {
            if (ModelState.IsValid)
            {
                await _context.Add(addproduct);
                return RedirectToAction(nameof(Index));

            }
            return View(addproduct);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {    
            var product = await _context.GetById(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(id, product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.GetById(id);
            if (product == null) return View("NotFound");
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var user = await _context.GetById(id);
            if (user == null) return View("NotFound");
            await _context.Delete(id);
            return RedirectToAction(nameof(Index));

        }


    }
}
