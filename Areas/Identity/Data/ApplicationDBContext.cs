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
            IngredientMeasurementUnit amount = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Grams };
            IngredientMeasurementUnit grams = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Grams };
            IngredientMeasurementUnit millilitres = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Millilitres };
            IngredientMeasurementUnit pinches = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Pinches };
            IngredientMeasurementUnit teaspoons = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Teaspoons };
            IngredientMeasurementUnit tablespoons = new IngredientMeasurementUnit() { Unit = MeasurementUnit.Tablespoons };

            List<Ingredient> ingredients = new()
            {
                new() { Name = "Pepper", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, pinches } },
                new() { Name = "Salt", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, pinches } },
                new() { Name = "Olive Oil", SupportedUnits = new List<IngredientMeasurementUnit>() { millilitres, teaspoons, tablespoons } },
                new() { Name = "Garlic", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Onion", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Tomato", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount} },
                new() { Name = "Pasta", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Basil", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Parmesan", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Bacon", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Eggs", SupportedUnits = new List<IngredientMeasurementUnit>() { amount, grams } },
                new() { Name = "Milk", SupportedUnits = new List<IngredientMeasurementUnit>() { millilitres } },
                new() { Name = "Butter", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Flour", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Sugar", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new() { Name = "Chocolate", SupportedUnits = new List<IngredientMeasurementUnit>() { grams } },
                new() { Name = "Vanilla", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Baking Powder", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new() { Name = "Baking Soda", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new() { Name = "Cinnamon", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new() { Name = "Nutmeg", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons, amount } },
                new() { Name = "Cocoa Powder", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, tablespoons, teaspoons } },
                new() { Name = "Chicken", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Beef", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Pork", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Fish", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Lamb", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount } },
                new() { Name = "Potatoes", SupportedUnits = new List<IngredientMeasurementUnit>() { grams, amount} }
            };

            context.AddRange(ingredients);
            context.SaveChanges();
        }

        if (!context.Recipes.Any() && context.Users.Any())
        {

            List<Recipe> recipes = new()
            {
                new Recipe
                {
                    Name = "Spaghetti Bolognese",
                    Description = "A classic Italian dish",
                    Instructions = "1. Heat the oil in a large saucepan and fry the onion and garlic for 5 mins until softened. ",
                    User = context.Users.First(),
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Salt"), Quantity = 1 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Olive Oil"), Quantity = 1 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Garlic"), Quantity = 5 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Onion"), Quantity = 50 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Tomato"), Quantity = 250 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Pasta"), Quantity = 250 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Basil"), Quantity = 5 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Parmesan"), Quantity = 20 }
                    }
                },
                new Recipe
                {
                    Name = "Carbonara",
                    Description = "A classic Italian dish",
                    Instructions =
                        "1. Heat the oil in a large saucepan and fry the onion and garlic for 5 mins until softened.",
                    User = context.Users.First(),
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Pepper"), Quantity = 1 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Salt"), Quantity = 1 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Olive Oil"), Quantity = 10 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Garlic"), Quantity = 1 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Onion"), Quantity = 100 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Pasta"), Quantity = 200 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Parmesan"), Quantity = 20 }
                    }
                },
                new Recipe
                {
                    Name = "Chocolate Cake",
                    Description = "Chocolate cake description",
                    Instructions = "1. Believe",
                    User = context.Users.First(),
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Eggs"), Quantity = 4 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Milk"), Quantity = 25 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Flour"), Quantity = 200 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Sugar"), Quantity = 100 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Cocoa Powder"), Quantity = 25 }
                    }
                },
                new Recipe
                {
                    Name = "Chocolate Chip Cookies",
                    Description = "Always allow cookies",
                    Instructions = "1. Remove cat from oven. 2. Bake dem.",
                    User = context.Users.First(),
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Eggs"), Quantity = 2 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Butter"), Quantity = 100 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Flour"), Quantity = 200 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Sugar"), Quantity = 100 },
                        new() { Ingredient = context.Ingredients.First(i => i.Name == "Chocolate"), Quantity = 100 }
                    }
                }
            };

            context.AddRange(recipes);
            context.SaveChanges();
        }



        if (!context.RecipeCollections.Any() && context.Users.Any())
        {
            List< RecipeCollection> recipeCollections = new()
            {
                new()
                {
                    Name = "Italian recipes",
                    Description = "A collection of Italian recipes",
                    User = context.Users.First(),
                    SavedRecipes = new List<Recipe>()
                    {
                        context.Recipes.First(r => r.Name == "Spaghetti Bolognese"),
                        context.Recipes.First(r => r.Name == "Carbonara")
                    }
                },
                new()
                {
                    Name = "Baking",
                    Description = "A collection of baking recipes",
                    User = context.Users.First(),
                    SavedRecipes = new List<Recipe>()
                    {
                        context.Recipes.First(r => r.Name == "Chocolate Cake"),
                        context.Recipes.First(r => r.Name == "Chocolate Chip Cookies")
                    }
                }
            };

            context.AddRange(recipeCollections);
            context.SaveChanges();
        }
    }
}
