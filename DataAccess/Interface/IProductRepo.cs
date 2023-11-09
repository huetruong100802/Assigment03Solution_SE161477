using BusinessObject.Models;
using BusinessObject.ViewModels;

namespace DataAccess.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<List<Category>> GetCategories();
        Task<List<Product>> SearchByNameAndPriceAsync(string? productName, decimal? unitPrice);

    }
}
