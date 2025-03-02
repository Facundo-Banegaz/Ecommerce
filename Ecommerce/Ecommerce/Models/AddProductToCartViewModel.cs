using Ecommerce.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class AddProductToCartViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(350, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Titulo Descripción")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string TituloDescription { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Stock { get; set; }




        [Display(Name = "Estado del Producto")]
        public bool Estate { get; set; } = true;

        // **Promoción**
        [Display(Name = "En Promoción")]
        public bool IsPromoted { get; set; }


        [Display(Name = "Descuento (%)")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal DiscountPercentage { get; set; }



        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();



        [Display(Name = "Calificación Promedio")]
        public decimal AverageRating => Ratings.Any() ? (decimal)Ratings.Average(rating => (int)rating) : 0;



        [Display(Name = "Precio Final")]
        public decimal FinalPrice => IsPromoted ? Price - (Price * (DiscountPercentage / 100)) : Price;


        [Display(Name = "Categorías")]
        public string Categories { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Range(0.0000001, float.MaxValue, ErrorMessage = "Debes de ingresar un valor mayor a cero en la cantidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]


        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

    }
}
