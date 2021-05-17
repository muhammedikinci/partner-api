using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;

namespace Repository.Repositories
{
    public class ProductRequestRepository : BaseRepository<ProductRequest>, IProductRequestRepository
    {
        public ProductRequestRepository(IDatabaseService dbService) : base(dbService)
        {
        }
    }
}