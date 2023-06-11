using System.Reflection;
using Task_Entities.Entities;

namespace Task_Client.ViewModel
{
    public class InvoiceViewModel
    {
        public string TitleOfInvoice { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
