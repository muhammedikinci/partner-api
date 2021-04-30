using Persistence;
using Persistence.Interfaces;
using MongoDB.Driver;

namespace Persistence
{
    public class DatabaseService : IDatabaseService
    {
        public IMongoDatabase database;

        public DatabaseService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }
    }
}