using BusinessObject.Models;
using BusinessObject.ViewModels.Category;

namespace BusinessObject.ViewModels.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public int? CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Weight { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
        public virtual CategoryViewModel? Category { get; set; }
    }
}
