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

        public List<string> Products;

        public string Status;

        public string Notes;

        public string PartnerId;

        public DateTime UpdatedAt;
    }
}