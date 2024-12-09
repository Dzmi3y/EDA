using EDA.Services.Catalog.Data;
using MongoDB.Driver;

namespace EDA.Services.Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<long> GetProductCountAsync()
        {
            return await _products.CountDocumentsAsync(Builders<Product>.Filter.Empty);
        }

        public async Task<IEnumerable<Product>> GetListAsync(int pageSize,int pageNumber)
        {
            var filter = Builders<Product>.Filter.Empty;

            return await _products.Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(); 
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }

        public async Task InitializeProductsAsync()
        {

            var productCount = await _products.CountDocumentsAsync(Builders<Product>.Filter.Empty);
            if (productCount == 0)
            {
                var initialProducts = MongoInit.GetProducts();
                await _products.InsertManyAsync(initialProducts);
            }
        }
    }

}
