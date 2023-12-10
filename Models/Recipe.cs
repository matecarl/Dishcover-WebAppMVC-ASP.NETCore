using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dishcover.Areas.Identity.Data;

namespace Dishcover.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(4000)")]
        public string Instructions { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? ImagePath { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; } = new List<RecipeIngredient>();
    }
}
