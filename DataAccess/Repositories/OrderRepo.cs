using BusinessObject.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(MyDBContext context) : base(context)
        {
        }
        public new async Task<List<Order>> GetAllAsync()
        {
            return await _context.Set<Order>().Include(x=>x.User).ToListAsync();
        }
        public async Task<List<Order>> GetAllAsync(string memberId)
        {
            var list = await _context.Set<Order>().Where(x => x.MemberId == memberId).Include(x => x.User).ToListAsync(); ;
            return list;
        }
        public new async Task<Order> GetByIdAsync(int id)
        {
            try
            {
                var item = await _dbSet.Include(x => x.User).SingleOrDefaultAsync(x=>x.OrderId==id);
                return item!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
