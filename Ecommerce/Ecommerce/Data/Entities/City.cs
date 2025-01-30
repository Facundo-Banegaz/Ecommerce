using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class City
    {
        public Guid Id { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public State State { get; set; }


        public ICollection<User>  Users {  get; set; } 
    }
}
