namespace EDA.Services.Сatalog.Data
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
        public int Count { get; set; }

    }
}
