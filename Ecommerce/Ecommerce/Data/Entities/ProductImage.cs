using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class ProductImage
    {

        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7232/images/no-camera.png"
            : $"https://todoshop.blob.core.windows.net/products/{ImageId}";

    }
}
