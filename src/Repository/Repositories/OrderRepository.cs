using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;

namespace Repository.Repositories
{
    public class OrderRepository : BaseRepository<Partner>, IOrderRepository
    {
        public OrderRepository(IDatabaseService dbService) : base(dbService)
        {
        }
    }
}