using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }
    [Range(0,10000)]
    public int Quantity { get; set; }

    public double Discount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
