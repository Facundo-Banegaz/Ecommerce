using Ecommerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Entities
{
    public class Rating
    {

        public int Id { get; set; }

        public Product Product { get; set; }


        public int ProductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Calificación")]
        [EnumDataType(typeof(RatingValue))]
        public RatingValue Value { get; set; }


        [Display(Name = "Comentarios")]
        [MaxLength(500)]
        public string? Comment { get; set; }


        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
