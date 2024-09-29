using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre del Cliente:")]
        [StringLength(150, MinimumLength = 4)]
        public string Nombre { get; set; }


        [Display(Name = "Apellido:")]
        [Required(ErrorMessage = "Por favor ingresar el Apellido del Cliente:")]
        [StringLength(150, MinimumLength = 4)]
        public string Apellido { get; set; }



        [Display(Name = "Ciudad:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre de la Ciudad:")]
        [StringLength(150, MinimumLength = 4)]
        public Ciudad Ciudad { get; set; }


        [Display(Name = "Sexo:")]
        [Required(ErrorMessage = "Es  obligatorio!!")]
        [EnumDataType(typeof(Sexo))]
        public Sexo Sexo   { get; set; }

        [Display(Name = "Telefono:")]
        [Required(ErrorMessage = "Por favor ingresar el Telefono del Cliente:")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Email es requerido.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [Display(Name = "Correo Electrónico")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La longitud debe estar entre 5 y 100 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [Display(Name = "Password"), PasswordPropertyText]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La longitud debe estar entre 5 y 100 caracteres")]
        public string Password{ get; set; }
    }
}
