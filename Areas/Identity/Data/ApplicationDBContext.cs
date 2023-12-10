using System.Diagnostics;
using Dishcover.Areas.Identity.Data;
using Dishcover.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dishcover.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; } = default!;
    public DbSet<Ingredient> Ingredients { get; set; } = default!;
    public DbSet<IngredientMeasurementUnit> IngredientMeasurementUnits { get; set; } = default!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;
    public DbSet<RecipeCollection> RecipeCollections { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public static void DataInitializer(ApplicationDBContext context)
    {

        if (!context.Ingredients.Any())
        {
            IngredientMeasurementUnit amount = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Grams};
            IngredientMeasurementUnit grams = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Grams };
            IngredientMeasurementUnit millilitres = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Millilitres };
            IngredientMeasurementUnit pinches = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Pinches };
            IngredientMeasurementUnit teaspoons = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Teaspoons };
            IngredientMeasurementUnit tablespoons = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Tablespoons };

            List<Ingredient> ingredients = new()
            {
                new Ingredient { Name = "Pepper", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, pinches } },
                new Ingredient { Name = "Salt", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, pinches } },
                new Ingredient { Name = "Olive Oil", SupportedUnits = new List<IngredientMeasurementUnit>() { millilitres, teaspoons, tablespoons } },
                new Ingredient { Name = "Garlic", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Onion", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Tomato", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount} },
                new Ingredient { Name = "Pasta", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Basil", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Parmesan", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Bacon", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Eggs", SupportedUnits = new List<IngredientMeasurementUnit>() { amount, grams } },
                new Ingredient { Name = "Milk", SupportedUnits = new List<IngredientMeasurementUnit>() { millilitres } },
                new Ingredient { Name = "Butter", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Flour", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Sugar", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new Ingredient { Name = "Chocolate", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new Ingredient { Name = "Vanilla", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Baking Powder", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new Ingredient { Name = "Baking Soda", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new Ingredient { Name = "Cinnamon", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new Ingredient { Name = "Nutmeg", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons, amount } },
                new Ingredient { Name = "Cocoa Powder", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new Ingredient { Name = "Chicken", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Beef", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Pork", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Fish", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Lamb", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new Ingredient { Name = "Potatoes", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount} }
            };

            context.AddRange(ingredients);
            context.SaveChanges();
        }
    }
}
