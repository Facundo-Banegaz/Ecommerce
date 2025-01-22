using Ecommerce.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class StateViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public Guid Countryid { get; set; }
    }
}
