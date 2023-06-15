using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? TitleOfProduct { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }

        public int? InvoiceId { get; set; }

        [StringLength(200)]
        public string? ImageUrl { get; set; }

        public int? Quentity { get; set; }

        [ForeignKey("InvoiceId")]
        [InverseProperty("Products")]
        public virtual Invoice? Invoice { get; set; }
    }
}
