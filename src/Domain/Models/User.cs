namespace Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PartnerId { get; set; }
    }
}