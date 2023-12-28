using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dishcover.Areas.Identity.Data;
using Dishcover.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Dishcover.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Recipes
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Recipes
                .Where(r => !r.DeletedAt.HasValue)
                .Include(r => r.User);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Recipes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            ViewData["Collections"] = new SelectList(
                _context.RecipeCollections
                    .Where(rc => rc.Userid == _userManager.GetUserId(HttpContext.User) && !rc.SavedRecipes.Contains(recipe)), "Id", "Name");

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["Ingredients"] = new SelectList(_context.Ingredients, "Id", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Instructions,IngredientInputs")] Recipe recipe)
        {
            recipe.UserId = _userManager.GetUserId(HttpContext.User);

            ModelState.ClearValidationState("UserId");
            await TryUpdateModelAsync(recipe);

            if (ModelState.IsValid)
            {
                foreach (var ingredient in recipe.IngredientInputs.Where(i => i.IngredientId != 0))
                {
                    recipe.Ingredients.Add(new RecipeIngredient()
                    {
                        Quantity = ingredient.Quantity,
                        IngredientId = ingredient.IngredientId
                    });
                }

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Ingredients"] = new SelectList(_context.Ingredients, "Id", "Name");
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Instructions")] Recipe recipeRequest)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = recipeRequest.Name;
            recipe.Description = recipeRequest.Description;
            recipe.Instructions = recipeRequest.Instructions;
            recipe.UpdatedAt = DateTime.Now;
            recipe.IngredientInputs = new List<RecipeIngredient>();

            ModelState.Clear();
            await TryUpdateModelAsync(recipe);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                    }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.DeletedAt = DateTime.Now;
                _context.Recipes.Update(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
