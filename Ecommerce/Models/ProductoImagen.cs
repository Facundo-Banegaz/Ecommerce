using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class ProductoImagen
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }



        [Required(ErrorMessage = "Por favor selecciona el producto.")]
        [Display(Name = "Producto")]
        public Producto Producto { get; set; }

    }
}
