using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public string StoreUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Sku { get; set; }

        public int Stock { get; set; }

        public string PartnerId { get; set; }
    }
}