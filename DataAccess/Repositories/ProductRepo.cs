using BusinessObject.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(MyDBContext context) : base(context)
        {
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Product>> SearchByNameAndPriceAsync(string? productName, decimal? unitPrice)
        {
            Expression<Func<Product, bool>> expression = x => x.ProductName.ToLower().Contains((productName ?? "").ToLower()) && x.UnitPrice == unitPrice;

            if (productName == null && unitPrice == null)
            {
                expression = x => true;
            }
            else if (productName == null)
            {
                expression = x => x.UnitPrice == unitPrice;
            }
            else if (unitPrice == null)
            {
                expression = x => x.ProductName.ToLower().Contains(productName.ToLower());
            }
            return await SearchBy(expression);
        }
        public async Task<List<Product>> SearchBy(Expression<Func<Product, bool>> expression)
        {
            var list = new List<Product>();
            try
            {
                list = await _context.Products.Include(x => x.Category).Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
