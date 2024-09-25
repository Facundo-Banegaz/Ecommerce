using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el nombre del Producto:")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Por favor ingresar la Descripción del Producto:")]
        public string Descripcion { get; set; }

        [Range(1, 15)]
        [Required(ErrorMessage = "Por favor ingrese el precio del Producto:")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio:")]
        public double Precio { get; set; }


        public int ProductoImagenId { get; set; }

        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public ProductoImagen ProductoImagen { get; set; }


        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor seleccionar una Categoría")]
        public virtual Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Por favor seleccionar el Estado")]
        public bool Estatus {  get; set; }

        [Display(Name = "Cantidad:")]
        [Required(ErrorMessage = "Por favor ingresar el Cantidad del Producto :")]
        public int Cantidad {  get; set; }
    }
}
