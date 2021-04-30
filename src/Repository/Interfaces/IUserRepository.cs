using Domain.Models;

namespace Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {
    }
}