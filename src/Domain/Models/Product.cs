using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Product : Entity
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Model { get; set; }

        public string Status { get; set; }

        public string PartnerId { get; set; }

        public List<Option> Options { get; set; }
    }
}