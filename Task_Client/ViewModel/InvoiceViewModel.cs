using System.Reflection;
using Task_Entities.Entities;

namespace Task_Client.ViewModel
{
    public class InvoiceViewModel
    {
        public string TitleOfInvoice { get; set; }
        public DateTime Date { get; set; }
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }
    }
}
