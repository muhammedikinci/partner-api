namespace Domain.ValueObjects
{
    public class StockUpdatePost
    {
        public string ProductId { get; set; }
        public string Token { get; set; }
        public string Stock { get; set; }
    }
}