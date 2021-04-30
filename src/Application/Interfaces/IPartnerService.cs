using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IPartnerService
    {
        IQueryable<Partner> GetAll();
        Partner GetById(string id);
        bool Add(Partner partner);
        bool Update(string id, Partner partner);
        bool Delete(string id);
        IQueryable<Product> GetProducts(string id);
        IQueryable<Order> GetOrders(string id);
    }
}