using System.ComponentModel.DataAnnotations;

namespace Aspiria.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        [Required]
        [MaxLength(50)]
        public string Company { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
    }
}