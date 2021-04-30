using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        IQueryable<Order> GetAll();
        Order GetById(string id);
        bool Add(Order order);
        bool Update(string id, Order order);
        bool Delete(string id);
        List<Product> GetProducts(string id);
        Partner GetPartner(string id);
    }
}