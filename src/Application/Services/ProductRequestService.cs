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
using Application.AppException.Exceptions;
using Application.AppException;

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
            ProductRequest productRequest = productRequestRepository.GetByIdAsync(id).Result;

            if (productRequest == null)
                throw new NotFoundException();

            return productRequest;
        }

        public ProductRequest GetByIdWithPartner(string id)
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            ProductRequest productRequest = productRequestRepository.Get(p => p.PartnerId == user.PartnerId && p.Id == id).FirstOrDefault();

            if (productRequest == null)
                throw new NotFoundException();

            return productRequest;
        }

        public bool Add(ProductRequest productRequest)
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            productRequest.PartnerId = user.PartnerId;
            productRequest.RequestStatus = new Domain.ValueObjects.ProductRequest.ProductRequestStatus()
            {
                Type = "İnceleme Bekliyor",
                Description = ""
            };
            productRequest.FixNecessary = false;

            ProductRequest p = productRequestRepository.AddAsync(productRequest).Result;
            return p != null;
        }

        public IQueryable<ProductRequest> GetAllMyRequests()
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            return productRequestRepository.Get(p => p.PartnerId == user.PartnerId);
        }

        public bool UpdateMyRequest(ProductRequest productRequest)
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            var request = productRequestRepository.Get(p => p.Id == productRequest.Id).FirstOrDefault();

            if (request == null)
                throw new NotFoundException();

            if (user.PartnerId != request.PartnerId)
                throw new PermissionDeniedException();

            if (!request.FixNecessary)
                throw new PermissionDeniedException();

            productRequest.PartnerId = request.PartnerId;
            productRequest.RequestStatus = request.RequestStatus;
            productRequest.FixNecessary = false;

            ProductRequest p = productRequestRepository.UpdateAsync(productRequest.Id, productRequest).Result;
            return p != null;
        }

        public bool Update(ProductRequest productRequest)
        {
            ProductRequest p = productRequestRepository.UpdateAsync(productRequest.Id, productRequest).Result;
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