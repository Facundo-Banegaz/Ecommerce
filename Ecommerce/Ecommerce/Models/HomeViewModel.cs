using Ecommerce.Data.Entities;

namespace Ecommerce.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        public float Quantity { get; set; }

    }
}
