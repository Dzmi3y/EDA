using EDA.Services.Catalog.Data;
using MongoDB.Bson;

namespace EDA.Services.Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<long> GetProductCountAsync();
        Task<IEnumerable<Product>> GetListAsync(int pageSize, int pageNumber);
        Task<Product> GetByIdAsync(Guid id); 
        Task AddAsync(Product product); 
        Task UpdateAsync(Product product); 
        Task DeleteAsync(Guid id);
        Task InitializeProductsAsync();
    }
}
