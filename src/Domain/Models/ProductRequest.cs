using Domain.ValueObjects.ProductRequest;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class ProductRequest : Entity
    {
        [Required]
        public string PartnerId { get; set; }
        
        public ProductRequestDetail ProductDetail { get; set; }

        [Required]
        public string PredictedStock { get; set; }

        [Required]
        public string ReadyForShippingDate { get; set; }

        public List<ProductRequestImage> Images { get; set; }
        public ProductRequestStatus RequestStatus { get; set; }

        [Required]
        public bool FixNecessary { get; set; }
    }
}