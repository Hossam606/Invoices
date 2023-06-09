using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Task_Entities.Entities;

[Table("Product")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Price { get; set; }

    public int? InvoiceId { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("Products")]
    public virtual Invoice? Invoice { get; set; }
}
