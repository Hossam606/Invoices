﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Task_Entities.Entities;

public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Total { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}