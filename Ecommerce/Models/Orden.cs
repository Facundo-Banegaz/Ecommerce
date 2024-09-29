using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Orden
    {
        [Key]
        public Guid Id { get; set; }


        [Display(Name = "Cliente:")]
        [Required(ErrorMessage = "Por favor selecciona el Nombre del Cliente:")]
        public Cliente Cliente { get; set; }


        [Display(Name = "Fecha Creacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }


        [Display(Name = "Fecha Update")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaUpdate { get; set; }



        //[Display(Name = "Precio:")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        //public decimal Precio { get; set; }

        public Guid Identificador { get; set; }

        [Display(Name = "Status:")]
        [Required(ErrorMessage = "Por favor selecciona el status de la Orden:")]
        public string Status { get; set; }

        [Display(Name = "Direccion:")]
        [Required(ErrorMessage = "Por favor selecciona la Direccion de la Orden:")]
        public string Direccion { get; set; }

        public List<OrdenProducto> OrdenProductos { get; set; }

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
    }

    
}
