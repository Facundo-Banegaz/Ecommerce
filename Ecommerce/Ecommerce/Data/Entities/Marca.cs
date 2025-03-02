using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class Marca
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} caractéres.")]

        public string Name { get; set; }


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }


        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7232/images/no-camera.png"
            : $"https://todoshop.blob.core.windows.net/categories/{ImageId}";



        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
