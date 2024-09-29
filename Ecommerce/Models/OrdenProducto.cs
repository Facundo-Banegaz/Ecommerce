using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrdenProducto
    {
        [Key]
        public Guid Id { get; set; }


        [Display(Name = "Producto:")]
        [Required(ErrorMessage = "Por favor selecciona el nombre del Producto:")]
        public Producto Producto { get; set; }

      


        [Display(Name = "Orden:")]
        [Required(ErrorMessage = "Por favor selecciona el orden :")]
        public Orden Orden { get; set; }





        [Display(Name = "precio:")]
        [Required(ErrorMessage = "Por favor selecciona el precio de la Orden:")]
        public double  precio { get; set; }
    }
}
