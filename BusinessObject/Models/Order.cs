using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class Order
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [ForeignKey("User")]
    public string? MemberId { get; set; }
    public virtual ApplicationUser User { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateTime OrderDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? RequiredDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? ShippedDate { get; set; }

    public decimal? Freight { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
