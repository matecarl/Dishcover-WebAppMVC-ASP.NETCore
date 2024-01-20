using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dishcover.Areas.Identity.Data;

namespace Dishcover.Models
{
    public class RecipeCollection
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        [Required]
        [Display(Name = "CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public string Userid { get; set; }
        [Display(Name = "Creator")]
        public ApplicationUser? User { get; set; }

        [Display(Name = "SavedRecipes")]
        public ICollection<Recipe> SavedRecipes { get; set; } = new List<Recipe>();
    }
}
