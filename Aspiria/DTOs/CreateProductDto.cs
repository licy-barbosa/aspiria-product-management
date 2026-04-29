using System.ComponentModel.DataAnnotations;

namespace Aspiria.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        public int? AgeRestriction { get; set; }

        [Required]
        [MaxLength(50)]
        public string Company { get; set; } = string.Empty;

        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
    }
}
