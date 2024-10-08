﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Por favor ingresar el Nombre de la Categoria:")]
        [StringLength(150, MinimumLength = 4)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        public ICollection<Producto>? Productos { get; set; }

    }
}
