using Microsoft.Build.Framework;

namespace Dishcover.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public int IngredientId { get; set; }

        public Ingredient? Ingredient { get; set; }
    }
}
