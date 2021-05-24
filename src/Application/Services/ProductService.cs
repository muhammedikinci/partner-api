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
using Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Application.Auth.Helpers;
using Application.AppException.Exceptions;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRequestService requestService;
        private readonly AppSettings appSettings;

        public ProductService(IUserRepository userRepository, IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IRequestService requestService, IOptions<AppSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.requestService = requestService;
            this.appSettings = appSettings.Value;
        }

        public IQueryable<Product> GetAll()
        {
            return productRepository.Get();
        }

        public IQueryable<Product> GetAllByPartnerId()
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            return productRepository.Get(o => o.PartnerId == user.PartnerId);
        }

        public Product GetById(string id)
        {
            return productRepository.GetByIdAsync(id).Result;
        }

        public bool Add(Product product)
        {
            Product p = productRepository.AddAsync(product).Result;
            return p != null;
        }

        public bool UpdateOrCreate(Product product)
        {
            Product p = productRepository.Get(p => p.ProductId == product.ProductId).FirstOrDefault();
            
            if (p == null)
            {
                return this.Add(product);
            }
            else
            {
                return this.Update(p.Id, product);
            }
        }

        public bool Update(string id, Product product)
        {
            product.Id = id;
            Product p = productRepository.UpdateAsync(id, product).Result;
            return p != null;
        }

        public bool UpdateStock(string id, StockUpdate stock)
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            Product p = productRepository.Get(p => p.Id == id && p.PartnerId == user.PartnerId).FirstOrDefault();
            p.Quantity = stock.Stock;
            var updated = productRepository.UpdateAsync(id, p).Result;

            if (updated == null)
            {
                return false;
            }

            StockUpdatePost stockUpdatePost = new StockUpdatePost();

            stockUpdatePost.Stock = stock.Stock;
            stockUpdatePost.Token = appSettings.StockUpdaterAPI.Token;
            stockUpdatePost.ProductId = p.ProductId;

            requestService.Post<StockUpdatePost>(appSettings.StockUpdaterAPI.UpdateStockURL, stockUpdatePost);

            return true;
        }

        public bool Delete(string id)
        {
            Product p = productRepository.DeleteAsync(id).Result;
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