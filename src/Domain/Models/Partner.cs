using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Partner : Entity
    {
        public string Name { get; set; }

        public string AccountId { get; set; }
    }
}