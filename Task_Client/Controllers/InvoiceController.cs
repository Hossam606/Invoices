using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Task_Client.ViewModel;
using Task_DAL.Data;
using Task_DAL.Repository;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_Client.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IHomeRepository<Invoice> _context;

        private readonly DbTaskContext _db;
        public InvoiceController(IHomeRepository<Invoice> context,DbTaskContext db)
        {
            _context= context;
            _db= db;
        }
        public async Task<IActionResult> Index()
        {


            //IEnumerable<InvoiceViewModel> query = await (from invoice in _db.Invoices
            //                                             join product in _db.Products on invoice.Id equals product.InvoiceId into products
            //                                             select new InvoiceViewModel
            //                                             {
            //                                                 Title = invoice.TitleOfInvoice,
            //                                                 Date = invoice.Date,
            //                                                 Products = products.Select(p => new Product
            //                                                 {
            //                                                     Title = p.TitleOfProduct,
            //                                                     Quantity = p.Quantity,
            //                                                     Price = p.Price
            //                                                 }).ToList()
            //                                             }).ToListAsync();

            //return View(query);

            //List<InvoiceViewModel> query = (from invoice in _db.Invoices
            //                         join product in _db.Products
            //                         on invoice.Id equals product.InvoiceId
            //                         select new InvoiceViewModel { 
            //                         Title= invoice.TitleOfInvoice,
            //                         Date = invoice.Date.HasValue ? invoice.Date.Value : DateTime.MinValue,
                                         
            //                         }).ToList();
            
            //return View(query);


            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel addInvoice)
        {
            if (ModelState.IsValid)
            {
                var newInvoice = new Invoice()
                {
                    Date= DateTime.Now,
                    TitleOfInvoice=addInvoice.Title.ToString(),
                };

                await _db.Invoices.AddAsync(newInvoice) ;
                await _db.SaveChangesAsync();

                foreach(var product in addInvoice.Products) 
                {
                    var prod = new Product()
                    {
                        InvoiceId = newInvoice.Id,
                        TitleOfProduct = product.TitleOfProduct,
                        Price= product.Price,
                        Quentity= product.Quentity,
                        ImageUrl= product.ImageUrl,
                    };
                    await _db.Products.AddAsync(prod);
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(addInvoice);
        }
    }
}
