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
    public class RecipeCollectionsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipeCollectionsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: RecipeCollections
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.RecipeCollections.Include(r => r.User);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: RecipeCollections/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RecipeCollections == null)
            {
                return NotFound();
            }

            var recipeCollection = await _context.RecipeCollections
                .Include(r => r.SavedRecipes)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipeCollection == null)
            {
                return NotFound();
            }

            return View(recipeCollection);
        }

        // GET: RecipeCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] RecipeCollection recipeCollection)
        {
            recipeCollection.Userid = _userManager.GetUserId(HttpContext.User);

            ModelState.ClearValidationState("UserId");
            await TryUpdateModelAsync(recipeCollection);

            if (ModelState.IsValid)
            {
                _context.Add(recipeCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeCollection);
        }

        // GET: RecipeCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RecipeCollections == null)
            {
                return NotFound();
            }

            var recipeCollection = await _context.RecipeCollections.FindAsync(id);
            if (recipeCollection == null)
            {
                return NotFound();
            }
            if (recipeCollection.Userid != _userManager.GetUserId(HttpContext.User) && !HttpContext.User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            return View(recipeCollection);
        }

        // POST: RecipeCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description")] RecipeCollection recipeCollectionRequest)
        {
            var recipeCollection = await _context.RecipeCollections.FirstOrDefaultAsync(rc => rc.Id == id);
            if (recipeCollection == null)
            {
                return NotFound();
            }
            if (recipeCollection.Userid != _userManager.GetUserId(HttpContext.User) && !HttpContext.User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            recipeCollection.Name = recipeCollectionRequest.Name;
            recipeCollection.Description = recipeCollectionRequest.Description;
            recipeCollection.UpdatedAt = DateTime.Now;

            ModelState.Clear();
            await TryUpdateModelAsync(recipeCollection);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipeCollection);
        }

        // GET: RecipeCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RecipeCollections == null)
            {
                return NotFound();
            }

            var recipeCollection = await _context.RecipeCollections
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeCollection == null)
            {
                return NotFound();
            }
            if (recipeCollection.Userid != _userManager.GetUserId(HttpContext.User) && !HttpContext.User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            return View(recipeCollection);
        }

        // POST: RecipeCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecipeCollections == null)
            {
                return Problem("Entity set 'ApplicationDBContext.RecipeCollections'  is null.");
            }
            var recipeCollection = await _context.RecipeCollections.FindAsync(id);
            if (recipeCollection != null)
            {
                if (recipeCollection.Userid != _userManager.GetUserId(HttpContext.User) && !HttpContext.User.IsInRole("Admin"))
                {
                    return Unauthorized();
                }
                _context.RecipeCollections.Remove(recipeCollection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe(int recipeId, int collectionId)
        {
            var collection = await _context.RecipeCollections
                .FirstOrDefaultAsync(rc => rc.Id == collectionId);
            if (collection == null)
            {
                return NotFound();
            }
            if (collection.Userid != _userManager.GetUserId(HttpContext.User))
            {
                return Unauthorized();
            }

            var recipe = _context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            if (collection.SavedRecipes.Contains(recipe))
            {
                return BadRequest();
            }

            collection.SavedRecipes.Add(recipe);
            _context.SaveChanges();

            return View(recipe);
        }
    }
}
