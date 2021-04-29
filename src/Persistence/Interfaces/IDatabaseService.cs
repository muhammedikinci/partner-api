using MongoDB.Driver;

namespace Persistence.Interfaces
{
    public interface IDatabaseService
    {
        IMongoDatabase GetDatabase();
    }
}