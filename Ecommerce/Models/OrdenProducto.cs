using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class OrdenProducto
    {
        [Key]
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public string Status { get; set; }

        public int Direccion { get; set; }
        public int Identificador { get; set; }
        public double  precio { get; set; }
    }
}
