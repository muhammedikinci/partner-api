using Domain.Models;

namespace Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product, string>
    {
    }
}