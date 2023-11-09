using BusinessObject.Models;
using BusinessObject.ViewModels.Cart;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.OrderDetail;
using BusinessObject.ViewModels.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Text.Json;

namespace eStore.Controllers
{
    public class CartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<JsonResult> AddToCartAsync(ProductViewModel model)
        {
            CartViewModel? cart;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string? str = HttpContext.Session.GetString(user.Id);
            var cartItem = new CartItemViewModel
            {
                ProductId = model.ProductId,
                UnitPrice = model.UnitPrice,
                Quantity = 1,
                Discount = 0,
                Product = model,
            };
            if (str != null)
            {
                cart = JsonSerializer.Deserialize<CartViewModel>(str);
                var item = cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (item != null)
                {
                    cartItem.Quantity = item.Quantity + 1;
                    cart.Items.Remove(item);
                }
                cart.Items.Add(cartItem);
            }
            else
            {
                cart = new CartViewModel
                {
                    UserId = user.Id,
                    Items = new List<CartItemViewModel> { cartItem },
                    Sum = 0,
                };
            }
            HttpContext.Session.SetString(user.Id, JsonSerializer.Serialize(cart));
            return Json(cart.Items.Count);
        }

        [HttpGet]
        public async Task<JsonResult> ViewCart()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string? str = HttpContext.Session.GetString(user.Id);
            if (str == null)
            {
                return Json(null);
            }
            var cart = JsonSerializer.Deserialize<CartViewModel>(str);
            cart.Sum = cart.Items.Sum(item=> item.UnitPrice * (decimal)(item.Quantity - item.Discount / 100));
            cart.Items=cart.Items.OrderBy(x => x.ProductId).ToList();
            return Json(cart);
        }
        [HttpGet] 
        public async Task<JsonResult> GetCartCount()
        {
            int count = 0;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string? str = HttpContext.Session.GetString(user.Id); 
            if (str == null)
            {
                return Json(count);
            }
            var cart = JsonSerializer.Deserialize<CartViewModel>(str);
            if(cart!=null && cart.Items.Count > 0)
            {
                count = cart.Items.Count;
            }
            return Json(count);
        }
        [HttpPut]
        public async Task<JsonResult> RemoveCartItem(int productId)
        {
            CartViewModel? cart = new ();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string? str = HttpContext.Session.GetString(user.Id); 
            if (str != null)
            {
                cart = JsonSerializer.Deserialize<CartViewModel>(str);
                var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    HttpContext.Session.SetString(user.Id, JsonSerializer.Serialize(cart));
                }
            }
            return Json(cart);
        }
        [HttpPost]
        public async Task<JsonResult> MakeOrder()
        {
            CartViewModel? cart;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string? str = HttpContext.Session.GetString(user.Id);
            cart = JsonSerializer.Deserialize<CartViewModel>(str);
            var order = new OrderViewModel
            {
                OrderDate = DateTime.Now,
                UserId = user.Id,
                OrderId = new Random().Next(),
            };
            foreach(var item in cart.Items)
            {
                var orderDetail = new OrderDetailViewModel
                {
                    Discount = item.Discount,
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                };
                order.OrderDetails.Add(orderDetail);
            }
            if (cart.Items.Count <= 0) throw new Exception();
            str = JsonSerializer.Serialize(order);
            cart.Items.Clear();
            HttpContext.Session.SetString(user.Id, JsonSerializer.Serialize(cart));
            return Json(str);
        }

    }
}
