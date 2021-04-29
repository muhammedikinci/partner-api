using Persistence.Interfaces;

namespace Persistence
{
    public class MongoDBSettings : IDatabaseSettings
    {
        public string PartnerCollectionName { get; set; }
        public string OrderCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}