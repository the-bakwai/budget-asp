using System.Threading.Tasks;
using BudgetAsp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAsp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var categories = await _repository.GetAllActiveOnly();
            
            return View(categories);
        }

        public IActionResult New()
        {
            var category = new Category();
            
            return View(category);
        }

        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("New");
            }
            else
            {
                category.UserId = 1;
                
                await _repository.Save(category);
                TempData["message"] = "Category Saved";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var category = await _repository.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public async Task<IActionResult> Update(long? id, Category category)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var editing = await _repository.Get(id.Value);

            if (editing == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            editing.Name = category.Name;

            await _repository.Save(editing);
            TempData["message"] = "Category Saved";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _repository.Delete(id.Value);
            TempData["message"] = "Category Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}