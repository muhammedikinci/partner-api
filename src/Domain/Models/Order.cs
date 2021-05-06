using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.ValueObjects;
using System.Collections.Generic;
using System;

namespace Domain.Models
{
    public class Order : Entity
    {
        public string PartnerId { get; set; }
        public string OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoicePrefix { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Address PaymentDetails { get; set; }
        public Address ShippingDetails { get; set; }
        public string Products { get; set; }
        public string Comment { get; set; }
        public string Total { get; set; }
        public string OrderStatus { get; set; }
        public string DateAdded { get; set; }
        public string DateModified { get; set; }
    }
}