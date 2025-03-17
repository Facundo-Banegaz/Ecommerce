using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(350, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Titulo Descripción")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string TitleDescription { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
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


        [Display(Name = "Producto Destacado")]
        public bool IsFeatured { get; set; }

        [Display(Name = "En Promoción")]
        public bool IsPromoted { get; set; }

        [Display(Name = "Marca")]
        public Brand Brand { get; set; }


        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Descuento (%)")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal DiscountPercentage { get; set; }



        [Display(Name = "Calificaciones")]
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        [Display(Name = "Calificación Promedio")]
        public decimal AverageRating => Ratings.Any() ? (decimal)Ratings.Average(r => (int)r.Value) : 0;



        [Display(Name = "Precio Final")]
        public decimal FinalPrice => IsPromoted ? Price - (Price * (DiscountPercentage / 100)) : Price;



        public ICollection<ProductCategory> ProductCategories { get; set; }

        [Display(Name = "Categorías")]
        public int CategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;

        public ICollection<ProductImage> ProductImages { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => ProductImages == null ? 0 : ProductImages.Count;


        [Display(Name = "Foto")]
        public string ImageFullPath => ProductImages == null || ProductImages.Count == 0
            ? $"https://localhost:7232/images/no-camera.png"
            : ProductImages.FirstOrDefault().ImageFullPath;

        public ICollection<Wishlist> Wishlist { get; set; } = new List<Wishlist>();
    }
}

