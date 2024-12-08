using EDA.Services.Catalog.Data;
using MongoDB.Driver;

namespace EDA.Services.Catalog
{
    public static class MongoInit
    {
        public static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product(Guid.NewGuid(), "African Bush Elephant", "Largest land animal", "http://localhost:83/images/products/e1.png", 5, 10),
                new Product(Guid.NewGuid(), "Asian Elephant", "Smaller ears, domesticated", "http://localhost:83/images/products/e2.png", 8, 9),
                new Product(Guid.NewGuid(), "African Forest Elephant", "Smaller, forest dweller", "http://localhost:83/images/products/e3.png", 10, 8),
                new Product(Guid.NewGuid(), "Sri Lankan Elephant", "Endemic to Sri Lanka", "http://localhost:83/images/products/e4.png", 2, 4),
                new Product(Guid.NewGuid(), "Indian Elephant", "Smaller, prominent hump", "http://localhost:83/images/products/e5.png", 13, 3),
                new Product(Guid.NewGuid(), "Sumatran Elephant", "Smallest Asian elephant", "http://localhost:83/images/products/e6.png", 5, 20),
                new Product(Guid.NewGuid(), "Borneo Elephant", "Pygmy, unique island species", "http://localhost:83/images/products/e7.png", 4, 5),
                new Product(Guid.NewGuid(), "Desert Elephant", "Adapted to desert life", "http://localhost:83/images/products/e8.png", 20, 7),

            };

            return products;
        }
    }
}
