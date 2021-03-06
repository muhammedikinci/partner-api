using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        IQueryable<Product> GetAllByPartnerId();
        Product GetById(string id);
        bool Add(Product product);
        bool UpdateOrCreate(Product product);
        bool Update(string id, Product product);
        bool UpdateStock(string id, Domain.ValueObjects.StockUpdate product);
        bool Delete(string id);
    }
}