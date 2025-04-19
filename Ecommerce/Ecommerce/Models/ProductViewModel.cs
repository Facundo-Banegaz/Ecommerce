using Ecommerce.Data.Entities;

namespace Ecommerce.Models
{
    public class ProductViewModel
    {
        public ICollection<Product> Products { get; set; }

        public int Quantity { get; set; }

    }
}
