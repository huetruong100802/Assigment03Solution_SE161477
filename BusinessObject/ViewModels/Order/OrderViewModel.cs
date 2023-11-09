using BusinessObject.Models;
using BusinessObject.ViewModels.OrderDetail;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModels.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string? UserId { get; set; }
        public string? UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RequiredDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }
        public virtual ICollection<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
    }
    public class OrderPatchModel
    {
        public int OrderId { get; set; }

        public string? UserId { get; set; }
        public string? UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RequiredDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }
        public virtual ICollection<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
    }
}
