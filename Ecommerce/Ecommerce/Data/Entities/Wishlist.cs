namespace Ecommerce.Data.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Product Product { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
