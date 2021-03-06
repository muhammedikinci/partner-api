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
using Application.AppException.Exceptions;
using Application.AppException;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
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
                throw new UserNotValidException(ExceptionConstants.USER_CLAIM_NOT_VALID);

            if (roleClaim.Value == Role.Admin)
            {
                Order _order = orderRepository.GetByIdAsync(id).Result;

                if (_order == null)
                    throw new OrderNotFoundException();
                else
                    return _order;
            }

            var user = userRepository.GetByIdAsync(idClaim.Value).Result;

            if (user == null)
                throw new UserNotValidException();

            Order order = orderRepository.GetAsync(o => o.Id == id && o.PartnerId == user.PartnerId).Result;

            if (order == null)
                throw new OrderNotFoundException();

            return order;
        }

        public IQueryable<Order> GetAllByPartnerId()
        {
            ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.First(c => c.Type == ClaimTypes.Name);

            var user = userRepository.GetByIdAsync(idClaim.Value).Result;

            if (user == null)
                throw new UserNotValidException();

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

        public bool DeleteManyOrdersByOrderId(string orderId)
        {
            var deleteResult = orderRepository.DeleteManyOrdersByOrderId(orderId).Result;

            if (deleteResult.DeletedCount == 0)
                return false;

            return true;
        }
    }
}