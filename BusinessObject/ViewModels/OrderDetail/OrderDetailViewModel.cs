using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModels.OrderDetail
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Range(0, 10000)]
        public int Quantity { get; set; }

        public double Discount { get; set; }

        //public virtual OrderViewModel Order { get; set; } = null!;

        public virtual ProductViewModel? Product { get; set; } = null!;
    }
}
