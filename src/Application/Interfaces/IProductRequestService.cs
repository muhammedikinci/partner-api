using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductRequestService
    {
        IQueryable<ProductRequest> GetAll();
        ProductRequest GetById(string id);
        bool Add(ProductRequest productRequest);
        IQueryable<ProductRequest> GetAllMyRequests();
        bool UpdateMyRequest(ProductRequest productRequest);
        bool Update(string id, ProductRequest productRequest);
        bool Delete(string id);
    }
}