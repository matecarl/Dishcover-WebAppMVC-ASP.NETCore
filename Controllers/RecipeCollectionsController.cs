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
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: RecipeCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Userid")] RecipeCollection recipeCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", recipeCollection.Userid);
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
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", recipeCollection.Userid);
            return View(recipeCollection);
        }

        // POST: RecipeCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Userid")] RecipeCollection recipeCollection)
        {
            if (id != recipeCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeCollectionExists(recipeCollection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", recipeCollection.Userid);
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
                _context.RecipeCollections.Remove(recipeCollection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeCollectionExists(int id)
        {
          return (_context.RecipeCollections?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe(int recipeId, int collectionId)
        {
            var collection = _context.RecipeCollections
                .FirstOrDefault(rc => rc.Id == collectionId);
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
