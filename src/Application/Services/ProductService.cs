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

        public bool UpdateStock(string id, int stock)
        {
            Product p = productRepository.GetByIdAsync(id).Result;
            p.Quantity = stock.ToString();
            var updated = productRepository.UpdateAsync(id, p).Result;
            return updated != null;
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