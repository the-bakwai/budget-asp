using System.Threading.Tasks;
using BudgetAsp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAsp.Controllers
{
    public class PayeesController : Controller
    {
        // GET
        private readonly IPayeeRepository _repository;

        public PayeesController(IPayeeRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var payees = await _repository.GetAllActiveOnly();
            
            return View(payees);
        }

        public IActionResult New()
        {
            var payee = new Payee();
            
            return View(payee);
        }

        public async Task<IActionResult> Create(Payee payee)
        {
            if (!ModelState.IsValid)
            {
                return View("New");
            }
            else
            {
                payee.UserId = 1;
                
                await _repository.Save(payee);
                TempData["message"] = "Payee Saved";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var payee = await _repository.Get(id.Value);

            if (payee == null)
            {
                return NotFound();
            }

            return View(payee);
        }

        public async Task<IActionResult> Update(long? id, Payee payee)
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

            editing.Name = payee.Name;

            await _repository.Save(editing);
            TempData["message"] = "Payee Saved";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _repository.Delete(id.Value);
            TempData["message"] = "Payee Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}