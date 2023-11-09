using AutoMapper;
using BusinessObject.Models;
using BusinessObject.ViewModels.Cart;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.OrderDetail;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eStoreAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartAPI : Controller
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;
        public ShoppingCartAPI(IOrderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostCartAsync(CartViewModel cart)
        {
            var order = new OrderViewModel
            {
                OrderDate = DateTime.Now,
                UserId = cart.UserId,
                OrderId = new Random().Next(),
            };
            foreach (var item in cart.Items)
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
            try
            {
                await _repo.AddAsync(_mapper.Map<Order>(order));
            }
            catch (DbUpdateException)
            {
                var m = await _repo.GetByIdAsync(order.OrderId);
                if (m != null)
                {
                    return Conflict();
                }
            }
            return CreatedAtAction("GetOrder","Orders", new { id = order.OrderId }, order);
        }
    }
}
