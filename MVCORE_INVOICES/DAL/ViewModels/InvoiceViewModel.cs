using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public string TitleOfInvoice { get; set; }
        public DateTime? Date { get; set; }
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }
    }
    public class ProductVM
    {
        public string TitleOfProduct { get; set; }
        public decimal? Price { get; set; }
        public int? InvoiceId { get; set; }
        public int? Quentity { get; set; }
        
    }


}
