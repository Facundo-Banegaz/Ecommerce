using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Orden
    {
        public int OrdenId { get; set; }

        [Display(Name = "Producto:")]
        [Required(ErrorMessage = "Por favor selecciona el nombre del Producto:")]
        public Producto Producto { get; set; }

        //[Display(Name = "Cliente:")]
        //[Required(ErrorMessage = "Por favor selecciona el Nombre del Cliente:")]
        //public Cliente Cliente { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Precio:")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio { get; set; }

        //public List<Item> Detalle { get; set; }
        //public decimal Total
        //{
        //    get
        //    {
        //        decimal _Total = 0;
        //        foreach (Item obj in Detalle)
        //        {
        //            _Total = _Total + obj.Importe;
        //        }
        //        return _Total;
        //    }
        //}

    }
}
