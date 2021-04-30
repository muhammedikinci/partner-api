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
    public class ProductService : IProductService
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IProductRepository productRepository;

        public ProductService(IPartnerRepository partnerRepository, IProductRepository productRepository)
        {
            this.partnerRepository = partnerRepository;
            this.productRepository = productRepository;
        }

        public IQueryable<Product> GetAll()
        {
            return productRepository.Get();
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

        public bool Update(string id, Product product)
        {
            product.Id = id;
            Product p = productRepository.UpdateAsync(id, product).Result;
            return p != null;
        }

        public bool Delete(string id)
        {
            Product p = productRepository.DeleteAsync(id).Result;
            return p != null;
        }

        public Partner GetPartner(string id)
        {
            Product product = productRepository.GetByIdAsync(id).Result;
            return partnerRepository.GetByIdAsync(product.PartnerId).Result;
        }
    }
}