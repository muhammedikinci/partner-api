using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.ValueObjects;
using System.Collections.Generic;
using System;

namespace Domain.Models
{
    public class Order : Entity
    {
        public Customer Customer { get; set; }

        public List<string> Products { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }

        public string PartnerId { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; }
    }
}