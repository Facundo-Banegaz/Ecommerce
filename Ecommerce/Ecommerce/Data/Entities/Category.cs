using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} caractéres.")]

        public string Name { get; set; }
    }
}
