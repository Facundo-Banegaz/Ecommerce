using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage ="El Campo {0} es obligatorio.")]
        [MaxLength(50,ErrorMessage ="El Campo {0} debe tener maximo {1} caractéres.")]
        
        public string Name { get; set; }

        public ICollection<State> States { get; set; } 

        [Display(Name = "Provincias")]
        public int StatesNumber => States == null ? 0 : States.Count;


    }
}
