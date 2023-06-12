using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IHomeRepository<Invoice> _Invoicecontext;
        private readonly IHomeRepository<Product> _Productcontext;
        private readonly DbTaskContext _db;
        public InvoiceController(IHomeRepository<Invoice> context,DbTaskContext db, IHomeRepository<Product> Productcontext)
        {
            _Invoicecontext= context;
            _db= db;
            _Productcontext= Productcontext;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<InvoiceViewModel> query = await (from invoice in _db.Invoices
                                            join product in _db.Products
                                            on invoice.Id equals product.InvoiceId
                                            select new InvoiceViewModel
                                            {
                                                TitleOfInvoice = invoice.TitleOfInvoice,
                                                Date = invoice.Date.HasValue ? invoice.Date.Value : DateTime.MinValue,
                                                Products = new List<Product>
                                                {
                                                    new Product
                                                    {
                                                        TitleOfProduct = product.TitleOfProduct,
                                                        Quentity = product.Quentity,
                                                        Price = product.Price,
                                                    }
                                                }

                                            }).ToListAsync();

            return View(query);


            //return View();
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
                    TitleOfInvoice=addInvoice.TitleOfInvoice,
                };

                await _Invoicecontext.Add(newInvoice);

                if(addInvoice.Products != null)
                {
                    foreach (var product in addInvoice.Products)
                    {
                        var newProduct = new Product()
                        {
                            InvoiceId = newInvoice.Id,
                            TitleOfProduct = product.TitleOfProduct,
                            Price = product.Price,
                            Quentity = product.Quentity,
                            ImageUrl = product.ImageUrl,
                        };
                        await _Productcontext.Add(newProduct);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else if(addInvoice.Product != null)
                {
                    var pro = new Product()
                    {
                        InvoiceId = newInvoice.Id,
                        TitleOfProduct = addInvoice.Product.TitleOfProduct,
                        Price = addInvoice.Product.Price,
                        Quentity = addInvoice.Product.Quentity,
                        
                    };
                    await _Productcontext.Add(pro);
                    return RedirectToAction(nameof(Index));
                    //return View("Index", "Invoice");
                }


                
            }

            return View(addInvoice);
        }
    }
}
