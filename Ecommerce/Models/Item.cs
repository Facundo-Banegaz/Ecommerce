namespace Ecommerce.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public ICollection<Orden>? Ordenes { get; set; }
    }
}
