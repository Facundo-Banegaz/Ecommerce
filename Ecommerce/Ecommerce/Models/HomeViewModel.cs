using Ecommerce.Data.Entities;

namespace Ecommerce.Models
{
    public class HomeViewModel
    {
        public List<Brand> Brands { get; set; }

        public List<Product> FeaturedProducts { get; set; }
        public List<Product> PromotedProducts {  get; set; }

    }
}
