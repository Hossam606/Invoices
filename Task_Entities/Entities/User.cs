using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Task_Entities.Entities;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? FullName { get; set; }

    [StringLength(50)]
    public string? UserName { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }
}
