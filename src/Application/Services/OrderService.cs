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
using System.Security.Claims;
using Application.Auth.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IPartnerRepository partnerRepository, IProductRepository productRepository, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.partnerRepository = partnerRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public IQueryable<Order> GetAll()
        {
            return orderRepository.Get();
        }

        public Order GetById(string id)
        {
            ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var roleClaim = identity.Claims.First(c => c.Type == ClaimTypes.Role);
            var idClaim = identity.Claims.First(c => c.Type == ClaimTypes.Name);
            
            if (roleClaim == null)
                return null;

            if (roleClaim.Value == Role.Admin)
                return orderRepository.GetByIdAsync(id).Result;

            var user = userRepository.GetByIdAsync(idClaim.Value).Result;

            if (user == null)
                return null;

            return orderRepository.GetAsync(o => o.Id == id && o.PartnerId == user.PartnerId).Result;
        }

        public IQueryable<Order> GetAllByPartnerId()
        {
            ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.First(c => c.Type == ClaimTypes.Name);

            var user = userRepository.GetByIdAsync(idClaim.Value).Result;

            if (user == null)
                return null;

            return orderRepository.Get(o => o.PartnerId == user.PartnerId);
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

        public Partner GetPartner(string id)
        {
            Order order = orderRepository.GetByIdAsync(id).Result;
            return partnerRepository.GetByIdAsync(order.PartnerId).Result;
        }
    }
}