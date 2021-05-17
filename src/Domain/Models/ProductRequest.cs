using Domain.ValueObjects.ProductRequest;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ProductRequest : Entity
    {
        public string PartnerId { get; set; }
        public ProductRequestDetail ProductDetail { get; set; }
        public string PredictedStock { get; set; }
        public string ReadyForShippingDate { get; set; }
        public List<ProductRequestImage> Images { get; set; }
        public ProductRequestStatus RequestStatus { get; set; }
    }
}