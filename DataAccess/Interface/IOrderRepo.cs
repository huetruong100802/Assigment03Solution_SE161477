using BusinessObject.Models;
using BusinessObject.ViewModels;

namespace DataAccess.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<List<Order>> GetAllAsync(string memberId);
    }
}
