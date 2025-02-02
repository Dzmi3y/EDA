using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Shared.Data
{
    public class CartItem
    {
        public CartItem(string id, decimal price, int count)
        {
            Id = id;
            Price = price;
            Count = count;
        }

        public string Id { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
