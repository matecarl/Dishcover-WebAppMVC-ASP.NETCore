using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dishcover.Areas.Identity.Data;
using Dishcover.Resources;
using Dishcover.Utilities;

namespace Dishcover.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Instructions")]
        [Column(TypeName = "nvarchar(4000)")]
        public string Instructions { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "ImagePath")]
        [Column(TypeName = "nvarchar(255)")]
        public string? ImagePath { get; set; }

        [Display(Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }

        public string UserId { get; set; }
        [Display(Name = "Creator")]
        public ApplicationUser? User { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();

        [NotMapped]
        [ValidIngredients]
        [Display(Name = "Ingredients")]
        public List<RecipeIngredient> IngredientInputs { get; set; }
    }
}
