using Domain.Models;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order, string>
    {
        Task<MongoDB.Driver.DeleteResult> DeleteManyOrdersByOrderId(string id);
    }
}