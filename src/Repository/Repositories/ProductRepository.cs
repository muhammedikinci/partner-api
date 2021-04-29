using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Partner>, IProductRepository
    {
        public ProductRepository(IDatabaseService dbService) : base(dbService)
        {
        }
    }
}