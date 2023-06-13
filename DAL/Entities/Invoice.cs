using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public partial class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }

        //[StringLength(50)]
        //public string? TitleOfInvoice { get; set; }

        [InverseProperty("Invoice")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
