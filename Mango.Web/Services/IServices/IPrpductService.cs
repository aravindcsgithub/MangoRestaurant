using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IPrpductService:IBaseService
    {
        Task<T> GetAllPrpductAsync<T>(string token);
        Task<T> GetProductsByIDAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDto productDto, string token);
        Task<T> UpdateProductAsync<T>(ProductDto productDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
