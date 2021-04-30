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
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public PartnerService(IPartnerRepository partnerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.partnerRepository = partnerRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public IQueryable<Partner> GetAll()
        {
            return partnerRepository.Get();
        }

        public Partner GetById(string id)
        {
            return partnerRepository.GetByIdAsync(id).Result;
        }

        public bool Add(Partner partner)
        {
            Partner p = partnerRepository.AddAsync(partner).Result;
            return p != null;
        }

        public bool Update(string id, Partner partner)
        {
            partner.Id = id;
            Partner p = partnerRepository.UpdateAsync(id, partner).Result;
            return p != null;
        }

        public bool Delete(string id)
        {
            Partner p = partnerRepository.DeleteAsync(id).Result;
            return p != null;
        }

        public IQueryable<Product> GetProducts(string id)
        {
            return productRepository.Get(p => p.PartnerId == id);
        }

        public IQueryable<Order> GetOrders(string id)
        {
            return orderRepository.Get(o => o.PartnerId == id);
        }
    }
}