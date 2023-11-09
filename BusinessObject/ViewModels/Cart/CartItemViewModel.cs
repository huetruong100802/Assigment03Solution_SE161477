using BusinessObject.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Range(0, 10000)]
        public int Quantity { get; set; }

        public double Discount { get; set; }

        public virtual ProductViewModel Product { get; set; } = null!;
    }
}
