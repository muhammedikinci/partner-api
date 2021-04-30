using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Repository.Interfaces;
using Repository.Repositories;
using Application.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public OrderService(IPartnerRepository partnerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.partnerRepository = partnerRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public IQueryable<Order> GetAll()
        {
            return orderRepository.Get();
        }

        public Order GetById(string id)
        {
            return orderRepository.GetByIdAsync(id).Result;
        }

        public bool Add(Order order)
        {
            Order o = orderRepository.AddAsync(order).Result;
            return o != null;
        }

        public bool Update(string id, Order order)
        {
            order.Id = id;
            Order o = orderRepository.UpdateAsync(id, order).Result;
            return o != null;
        }

        public bool Delete(string id)
        {
            Order o = orderRepository.DeleteAsync(id).Result;
            return o != null;
        }

        public List<Product> GetProducts(string id)
        {
            Order order = orderRepository.GetByIdAsync(id).Result;

            List<Product> products = new List<Product>();

            foreach (string productId in order.Products)
            {
                Product product = productRepository.GetByIdAsync(productId).Result;
                products.Add(product);
            }

            return products;
        }

        public Partner GetPartner(string id)
        {
            Order order = orderRepository.GetByIdAsync(id).Result;
            return partnerRepository.GetByIdAsync(order.PartnerId).Result;
        }
    }
}