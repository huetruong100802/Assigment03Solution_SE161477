﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Cart
{
    public class CartViewModel
    {
        public string? UserId { get; set; }
        public decimal Sum { get; set; }
        public virtual IList<CartItemViewModel> Items { get; set; }
    }
}
