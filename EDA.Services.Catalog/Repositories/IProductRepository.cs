using EDA.Shared.Data;

namespace EDA.Services.Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<long> GetProductCountAsync();
        Task<IEnumerable<Product>> GetListAsync(int pageSize, int pageNumber);
        Task<Product> GetByIdAsync(string id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
        Task InitializeProductsAsync();
    }
}
