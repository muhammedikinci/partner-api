using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Product : Entity
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string PartnerId { get; set; }

        public List<Option> Options { get; set; }
    }
}