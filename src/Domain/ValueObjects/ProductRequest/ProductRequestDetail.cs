using System.Collections.Generic;

namespace Domain.ValueObjects.ProductRequest
{
    public class ProductRequestDetail
    {
        public string Name { get; set; }
        public List<ProductRequestSize> Sizes { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
    }
}