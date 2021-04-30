using Persistence.Interfaces;

namespace Persistence
{
    public class MongoDBSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}