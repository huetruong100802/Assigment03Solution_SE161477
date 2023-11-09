using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public string? ProductName { get; set; }
        public string? UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; } = default!;
    }
}
