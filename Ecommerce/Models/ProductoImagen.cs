using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductoImagen
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }


        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Por favor selecciona el producto.")]
        [Display(Name = "Producto")]
        public Producto Producto { get; set; }

    }
}
