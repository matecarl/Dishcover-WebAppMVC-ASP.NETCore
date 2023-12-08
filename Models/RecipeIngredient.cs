using Microsoft.Build.Framework;

namespace Dishcover.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
