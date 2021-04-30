using Domain.Models;
using Repository.Interfaces;
using Persistence.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseService dbService) : base(dbService)
        {
        }
    }
}