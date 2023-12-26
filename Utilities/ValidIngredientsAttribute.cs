using Dishcover.Areas.Identity.Data;
using Dishcover.Models;
using System.ComponentModel.DataAnnotations;

namespace Dishcover.Utilities
{
    public class ValidIngredientsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<RecipeIngredient> ingredients = value as List<RecipeIngredient>;
            var _context = (ApplicationDBContext)validationContext
                 .GetService(typeof(ApplicationDBContext));

            for (int i = 0; i < ingredients.Count(); i++)
            {
                if (ingredients[i].IngredientId == 0)
                {
                    //if the ingredient is not specified (null), we avoid the following checks
                }
                else if (ingredients[i].Quantity <= 0)
                {
                    return new ValidationResult("Each ingredient's quantity must exceed 0");
                }
                else if (_context.Ingredients.Find(ingredients[i].IngredientId) == null)
                {
                    return new ValidationResult("One or more ingredient(s) have not been found in our records");
                }
            }

            return ValidationResult.Success;
        }
    }
}
