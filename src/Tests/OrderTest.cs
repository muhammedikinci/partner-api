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
    public class OrderTest
    {
        private DependencyResolverHelper _serviceProvider;
        private readonly IOrderService orderService;

        public OrderTest()
        {
            _serviceProvider = new DependencyResolverHelper();
            orderService = _serviceProvider.GetService<IOrderService>();
        }

        [Fact]
        public void GetAll_Orders_ReturnFilledList()
        {
            IQueryable<Order> orders = orderService.GetAll();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public void Add_AnOrder_ReturnTrue()
        {
            bool result = orderService.Add(new Order(){
                OrderId="delete"
            });

            Assert.True(result);

            Order order = orderService.GetAll().AsEnumerable().Last();

            orderService.Delete(order.Id);
        }

        [Fact]
        public void Update_AnOrder_ReturnTrue()
        {
            Order order = orderService.GetAll().First();
            order.OrderId = "111";
            bool result = orderService.Update(order.Id, order);

            Assert.True(result);

            order = orderService.GetAll().First();

            Assert.Equal("111", order.OrderId);

            order.OrderId = "000";
            orderService.Update(order.Id, order);
        }

        [Fact]
        public void Delete_AnOrder_ReturnTrue()
        {
            Order order = orderService.GetAll().AsEnumerable().Last();

            bool result = orderService.Delete(order.Id);

            Assert.True(result);

            orderService.Add(order);
        }
    }
}
