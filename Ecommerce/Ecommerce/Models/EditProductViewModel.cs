using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class EditProductViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Titulo Descripción")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string TitleDescription { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Stock { get; set; }
        [Display(Name = "Descuento (%)")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Estado del Producto")]
        public bool Estate { get; set; } = true;

        [Display(Name = "Producto Destacado")]
        public bool IsFeatured { get; set; }

        [Display(Name = "En Promoción")]
        public bool IsPromoted { get; set; }

        

        [Display(Name = "Marca")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Marca.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
