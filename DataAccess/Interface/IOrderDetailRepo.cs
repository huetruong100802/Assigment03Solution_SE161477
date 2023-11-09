using BusinessObject.Models;
using BusinessObject.ViewModels;

namespace DataAccess.Interfaces
{
    public interface IOrderDetailRepo : IGenericRepo<OrderDetail>
    {
        Task<OrderDetail> GetByIdAsync(int orderId, int productId);
        Task<List<OrderDetail>> GetByOrderIdAsync(int orderId);
    }
}
