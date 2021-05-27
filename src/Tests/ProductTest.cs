using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Domain.Models;
using Application.Interfaces;
using WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;

namespace Tests
{
    public class ProductTest
    {
        private DependencyResolverHelper _serviceProvider;
        private readonly IProductService productService;

        public ProductTest()
        {
            _serviceProvider = new DependencyResolverHelper();
            productService = _serviceProvider.GetService<IProductService>();
        }

        [Fact]
        public void GetAll_Products_ReturnFilledList()
        {
            IQueryable<Product> products = productService.GetAll();

            Assert.NotEmpty(products);
        }

        [Fact]
        public void Add_AProduct_ReturnTrue()
        {
            bool result = productService.Add(new Product(){
                ProductId="delete"
            });

            Assert.True(result);

            Product product = productService.GetAll().AsEnumerable().Last();

            productService.Delete(product.Id);
        }

        [Fact]
        public void Update_AProduct_ReturnTrue()
        {
            Product product = productService.GetAll().First();
            product.ProductId = "111";
            bool result = productService.Update(product.Id, product);

            Assert.True(result);

            product = productService.GetAll().First();

            Assert.Equal("111", product.ProductId);

            product.ProductId = "000";
            productService.Update(product.Id, product);
        }

        [Fact]
        public void Delete_AProduct_ReturnTrue()
        {
            Product product = productService.GetAll().AsEnumerable().Last();

            bool result = productService.Delete(product.Id);

            Assert.True(result);

            productService.Add(product);
        }
    }
}
