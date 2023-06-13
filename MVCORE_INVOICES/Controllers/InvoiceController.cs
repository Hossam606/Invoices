using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Infrastructure.IRepository;
using DAL.Context;
using DAL.ViewModels;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace MVCORE_INVOICES.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _Invoicecontext;
        private readonly IProductRepository _Productcontext;
        private readonly DBContext_Invoices _db;
        public InvoiceController(IInvoiceRepository InvoiceContext, DBContext_Invoices db, IProductRepository Productcontext)
        {
            _Invoicecontext = InvoiceContext;
            _Productcontext = Productcontext;
            _db = db;
        }
        public  IActionResult Index()
        {

            List<InvoiceViewModel> invoices = new List<InvoiceViewModel>();
            try
            {
                using (DBContext_Invoices socpaDbContext = new DBContext_Invoices(Helper.InvoicesHelper.GetDbContextOptions()))
                {

                    var invoicesLst = socpaDbContext.Invoices.Include("Products").ToList();
                    foreach (var invoice in invoicesLst)
                    {
                        InvoiceViewModel invoiceViewModel = new InvoiceViewModel();

                        List<Product> products = new List<Product>();
                        invoiceViewModel.Id = invoice.Id;
                        invoiceViewModel.Date = invoice.Date;
                        foreach (var product in invoice.Products)
                        {
                            Product product1 = new Product();
                            product1.Id = product.Id;
                            product1.Price = product.Price;
                            product1.Quentity = product.Quentity;
                            product1.ImageUrl = product.ImageUrl;
                            product1.TitleOfProduct = product.TitleOfProduct;
                            product1.InvoiceId = product.InvoiceId;
                            products.Add(product1);
                        }
                        invoiceViewModel.Products = (products);
                        invoices.Add(invoiceViewModel);
                    }

                }

            }
            catch (Exception ex)
            {
                return View(invoices);
                throw ex;
            }
            return View(invoices);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<Product> MyProducts)
        {
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
            invoiceViewModel.Products = MyProducts;
            if (ModelState.IsValid)
            {
                var newInvoice = new Invoice()
                {
                    Date = DateTime.Now,
                };

                await _Invoicecontext.Add(newInvoice);

                if (MyProducts != null)
                {
                    foreach (var product in MyProducts)
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
                    return RedirectToAction("Index","Invoice");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
            try
            {
                using (DBContext_Invoices socpaDbContext = new DBContext_Invoices(Helper.InvoicesHelper.GetDbContextOptions()))
                {
                    Invoice invoice = await _Invoicecontext.GetById(id);
                    List<Product> products = _Productcontext.GetAll().Where(x => x.InvoiceId == invoice.Id).ToList();
                    invoiceViewModel.Id = invoice.Id;
                    invoiceViewModel.Date = invoice.Date;
                    invoiceViewModel.Products = products;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(invoiceViewModel);

        }


    }
}
