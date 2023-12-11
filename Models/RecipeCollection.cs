using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dishcover.Areas.Identity.Data;

namespace Dishcover.Models
{
    public class RecipeCollection
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";

        public string Userid { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Recipe> SavedRecipes { get; set; } = new List<Recipe>();
    }
}
