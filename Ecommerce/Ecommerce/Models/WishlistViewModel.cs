using Ecommerce.Data.Entities;

namespace Ecommerce.Models
{
    public class WishlistViewModel
    {
       

        public User User { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; }
    }
}
