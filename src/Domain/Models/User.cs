using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        [Required]
        public string PartnerId { get; set; }

        [Required]
        public string TradeRegistryTitle { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string TaxNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string OwnerName { get;set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string CompanyPhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public int LoginAttemps { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastLoginAttempsAt { get; set; }
    }
}