using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class User : Entity
    {
        [BsonRequiredAttribute]
        public string Name { get; set; }

        [BsonRequiredAttribute]
        public string UserName { get; set; }

        [BsonRequiredAttribute]
        public string Password { get; set; }

        [BsonRequiredAttribute]
        public string Role { get; set; }

        [BsonRequiredAttribute]
        public string PartnerId { get; set; }

        public string TradeRegistryTitle { get; set; }

        public string RegistrationNumber { get; set; }

        public string TaxNumber { get; set; }

        public string Address { get; set; }

        public string OwnerName { get;set; }

        public string MobileNumber { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string Email { get; set; }
    }
}