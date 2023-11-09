using BusinessObject.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderDetailRepo : GenericRepo<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(MyDBContext context) : base(context)
        {
        }
        public new async Task<OrderDetail> GetByIdAsync(int id)
        {
            try
            {
                var item = await _dbSet.FindAsync(id);
                return item!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OrderDetail> GetByIdAsync(int orderId, int productId)
        {
            var item = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            return item!;
        }
        public async Task<List<OrderDetail>> GetByOrderIdAsync(int orderId)
        {
            var item = await _context.OrderDetails.Where(x => x.OrderId == orderId).Include(x => x.Product).ToListAsync();
            return item;
        }
        public async Task<List<OrderDetail>> GetByOProductIdAsync(int productId)
        {
            var item = await _context.OrderDetails.Where(x => x.ProductId == productId).ToListAsync();
            return item;
        }
    }
}
