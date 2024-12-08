namespace EDA.Services.Catalog.Data
{
    public class Product
    {
        public Product(Guid id, string title, string description, string imageUrl,
            decimal price, int count)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Count = count;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
        public int Count { get; set; }
    }
}
