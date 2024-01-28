namespace Dishcover.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public double Quantity { get; set; }

        public int IngredientId { get; set; }

        public Ingredient? Ingredient { get; set; }
    }
}
