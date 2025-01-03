using EDA.Shared.Data;

namespace EDA.Services.Catalog
{
    public static class MongoInit
    {
        public static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product(Guid.NewGuid().ToString(), "African Bush Elephant", "Largest land animal", "http://localhost:83/images/products/e1.png", 5, 10),
                new Product(Guid.NewGuid().ToString(), "Asian Elephant", "Smaller ears, domesticated", "http://localhost:83/images/products/e2.png", 8, 9),
                new Product(Guid.NewGuid().ToString(), "African Forest Elephant", "Smaller, forest dweller", "http://localhost:83/images/products/e3.png", 10, 8),
                new Product(Guid.NewGuid().ToString(), "Sri Lankan Elephant", "Endemic to Sri Lanka", "http://localhost:83/images/products/e4.png", 2, 4),
                new Product(Guid.NewGuid().ToString(), "Indian Elephant", "Smaller, prominent hump", "http://localhost:83/images/products/e5.png", 13, 3),
                new Product(Guid.NewGuid().ToString(), "Sumatran Elephant", "Smallest Asian elephant", "http://localhost:83/images/products/e6.png", 5, 20),
                new Product(Guid.NewGuid().ToString(), "Borneo Elephant", "Pygmy, unique island species", "http://localhost:83/images/products/e7.png", 4, 5),
                new Product(Guid.NewGuid().ToString(), "Desert Elephant", "Adapted to desert life", "http://localhost:83/images/products/e8.png", 20, 7),

            };

            return products;
        }
    }
}
