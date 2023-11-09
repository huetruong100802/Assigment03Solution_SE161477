using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using AutoMapper;
using BusinessObject.ViewModels.OrderDetail;
using Microsoft.AspNetCore.Authorization;

namespace eStoreAPI.Controllers
{
    [Route("api/orders/details")]
    [ApiController]
    public class OrderDetailsAPI : ControllerBase
    {
        private readonly IOrderDetailRepo _repo;
        private readonly IMapper _mapper;

        public OrderDetailsAPI(IMapper mapper, IOrderDetailRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailViewModel>>> GetOrderDetailsAsync()
        {
            if (await _repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<OrderDetailViewModel>>(await _repo.GetAllAsync());
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDetailViewModel>>> GetOrderDetailByOrderIdAsync(int id)
        {
            var list = await _repo.GetByOrderIdAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<OrderDetailViewModel>>(list);
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetailAsync(OrderDetailViewModel orderDetail)
        {
            await _repo.UpdateAsync(_mapper.Map<OrderDetail>(orderDetail));
            return NoContent();
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetailViewModel>> PostOrderDetailAsync(OrderDetailViewModel orderDetail)
        {
            try
            {
                await _repo.AddAsync(_mapper.Map<OrderDetail>(orderDetail));
            }
            catch (DbUpdateException)
            {
                var m = await _repo.GetByIdAsync(orderDetail.OrderId, orderDetail.ProductId);
                if (m != null)
                {
                    return Conflict();
                }
            }
            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailAsync(int orderId, int productId)
        {
            var m = await _repo.GetByIdAsync(orderId, productId);
            if (m == null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(m);
            return NoContent();
        }
    }
}
