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
using BusinessObject.ViewModels.Product;
using BusinessObject.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;

namespace eStoreAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    //[Authorize]
    public class ProductsAPI : ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;

        public ProductsAPI(IMapper mapper, IProductRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsAsync(string? productName, decimal? unitPrice)
        {
            if (await _repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductViewModel>>(await _repo.SearchByNameAndPriceAsync(productName, unitPrice));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategoriesAsync()
        {
            if (await _repo.GetCategories() == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<CategoryViewModel>>(await _repo.GetCategories());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductViewModel>(m);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAsync(int id, ProductViewModel product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            await _repo.UpdateAsync(_mapper.Map<Product>(product));
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProductAsync(ProductCreateModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                var list = await _repo.GetAllAsync();
                if (list.Count<=0)
                {
                    product.ProductId = 0;
                }
                else
                {
                    product.ProductId = list.Last().ProductId + 1;
                }
                await _repo.AddAsync(product);
                return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
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
