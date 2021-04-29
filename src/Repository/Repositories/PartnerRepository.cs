using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;

namespace Repository.Repositories
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(IDatabaseService dbService) : base(dbService)
        {
        }
    }
}