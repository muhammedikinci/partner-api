using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.ValueObjects;
using System.Collections.Generic;
using System;

namespace Domain.Models
{
    public class Order : Entity
    {
        public Customer Customer;

        [BsonRepresentation(BsonType.Array)]
        public List<string> Products;

        public string Status;

        public string Notes;

        public DateTime UpdatedAt;
    }
}