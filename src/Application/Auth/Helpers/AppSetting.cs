namespace Application.Auth.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public StockUpdaterAPI StockUpdaterAPI { get; set; }
    }

    public class StockUpdaterAPI
    {
        public string Token { get; set; }
        public string UpdateStockURL { get; set; }
    }
}