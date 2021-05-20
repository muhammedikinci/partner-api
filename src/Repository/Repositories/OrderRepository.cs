using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Repository.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseService dbService) : base(dbService)
        {
        }

        public async Task<MongoDB.Driver.DeleteResult> DeleteManyOrdersByOrderId(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderId);
            return await Collection.DeleteManyAsync(filter);
        }
    }
}