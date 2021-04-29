namespace Persistence.Interfaces
{
    public interface IDatabaseSettings
    {
        string PartnerCollectionName { get; set; }
        string OrderCollectionName { get; set; }
        string ProductCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}