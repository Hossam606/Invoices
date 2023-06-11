using System.Reflection;
using Task_Entities.Entities;

namespace Task_Client.ViewModel
{
    public class InvoiceViewModel
    {
        public int Title { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
