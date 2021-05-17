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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class ProductRequestService : IProductRequestService
    {
        private readonly IProductRequestRepository productRequestRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;

        public ProductRequestService(IProductRequestRepository productRequestRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.productRequestRepository = productRequestRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public IQueryable<ProductRequest> GetAll()
        {
            return productRequestRepository.Get();
        }

        public ProductRequest GetById(string id)
        {
            return productRequestRepository.GetByIdAsync(id).Result;
        }

        public bool Add(ProductRequest productRequest)
        {
            ProductRequest p = productRequestRepository.AddAsync(productRequest).Result;
            return p != null;
        }

        public IQueryable<ProductRequest> GetAllMyRequests()
        {
            var user = GetUserDataFromToken();

            if (user == null)
                return null;

            return productRequestRepository.Get(p => p.PartnerId == user.PartnerId);
        }

        public bool UpdateMyRequest(ProductRequest productRequest)
        {
            var user = GetUserDataFromToken();

            if (user == null)
                return false;

            var request = productRequestRepository.Get(p => p.Id == productRequest.Id).FirstOrDefault();

            if (request == null)
                return false;

            if (user.PartnerId != request.PartnerId)
                return false;

            ProductRequest p = productRequestRepository.UpdateAsync(productRequest.Id, productRequest).Result;
            return p != null;
        }

        public bool Update(string id, ProductRequest productRequest)
        {
            productRequest.Id = id;
            ProductRequest p = productRequestRepository.UpdateAsync(id, productRequest).Result;
            return p != null;
        }

        public bool Delete(string id)
        {
            ProductRequest p = productRequestRepository.DeleteAsync(id).Result;
            return p != null;
        }

        public Domain.Models.User GetUserDataFromToken()
        {
            ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.First(c => c.Type == ClaimTypes.Name);

            return userRepository.GetByIdAsync(idClaim.Value).Result;
        }
    }
}