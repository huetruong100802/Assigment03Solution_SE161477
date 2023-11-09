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
using BusinessObject.ViewModels.Order;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersAPI : ControllerBase
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;

        public OrdersAPI(IMapper mapper, IOrderRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersAsync()
        {
            if (await _repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<OrderViewModel>>(await _repo.GetAllAsync());
        }
        
        [HttpGet("MembersOrder/{id}")]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersAsync(string id)
        {
            if (await _repo.GetAllAsync(id) == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<OrderViewModel>>(await _repo.GetAllAsync(id));
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrderAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrderViewModel>(m);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrderAsync(int id, OrderViewModel model)
        {
            if (id != model.OrderId)
            {
                return BadRequest();
            }
            var order = await _repo.GetByIdAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            order = _mapper.Map(model, order);
            await _repo.UpdateAsync(order);
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> PostOrderAsync(OrderViewModel model)
        {
            try
            {
                var order = _mapper.Map<Order>(model);
                await _repo.AddAsync(order);
            }
            catch (DbUpdateException)
            {
                var m = await _repo.GetByIdAsync(model.OrderId);
                if (m != null)
                {
                    return Conflict();
                }
            }
            return CreatedAtAction("GetOrder", new { id = model.OrderId }, model);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(m);
            return NoContent();
        }
    }
}
