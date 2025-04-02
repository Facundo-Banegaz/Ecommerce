
using Ecommerce.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class BrandViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} caractéres.")]

        public string Name { get; set; }


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }


        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7232/images/no-camera.png"
            : $"https://todoshop.blob.core.windows.net/brands/{ImageId}";


        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
