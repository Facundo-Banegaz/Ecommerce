using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Comprobante
    {
        [Key]
        public int Id { get; set; } 



        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int ProductoId { get; set; }

        //[Display(Name = "Producto:")]
        //[Required(ErrorMessage = "Por favor selecciona el nombre del Producto:")]
        //public Producto Producto { get; set; }



        public int TotalCantidad { get; set; }


        public double TotalPrecio { get; set; }

    }
}
