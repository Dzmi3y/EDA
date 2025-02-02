﻿namespace EDA.Shared.Data
{
    public class Product
    {
        public Product(string id, string title, string description, string imageUrl,
            decimal price, int count)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Count = count;
        }


        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
