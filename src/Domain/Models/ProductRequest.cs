using Domain.ValueObjects.ProductRequest;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class ProductRequest : Entity
    {
        [BsonRequiredAttribute]
        public string PartnerId { get; set; }

        [BsonRequiredAttribute]
        public ProductRequestDetail ProductDetail { get; set; }

        [BsonRequiredAttribute]
        public string PredictedStock { get; set; }

        [BsonRequiredAttribute]
        public string ReadyForShippingDate { get; set; }

        [BsonRequiredAttribute]
        public List<ProductRequestImage> Images { get; set; }
        public ProductRequestStatus RequestStatus { get; set; }
        public bool FixNecessary { get; set; }
    }
}