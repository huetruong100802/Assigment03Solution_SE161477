using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string ProductName { get; set; } = null!;

    public string Weight { get; set; } = null!;

    [Required]
    public decimal UnitPrice { get; set; }

    [Required]
    public int UnitsInStock { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
