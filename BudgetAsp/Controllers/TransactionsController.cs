using System.Threading.Tasks;
using BudgetAsp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAsp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _repository;

        public TransactionsController(ITransactionRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var transactions = await _repository.GetAllActiveOnly();
            
            return View(transactions);
        }

        // public IActionResult New()
        // {
        //     var transaction = new Transaction();
        //     
        //     return View(transaction);
        // }
        //
        // public async Task<IActionResult> Create(Transaction transaction)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(nameof(New));
        //     }
        //     else
        //     {
        //         transaction.UserId = 1;
        //         
        //         await _repository.Save(transaction);
        //         TempData["message"] = "Transaction Saved";
        //         return RedirectToAction(nameof(Index));
        //     }
        // }
        //
        // public async Task<IActionResult> Edit(long? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     var transaction = await _repository.Get(id.Value);
        //
        //     if (transaction == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(transaction);
        // }
        //
        // public async Task<IActionResult> Update(long? id, Transaction transaction)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     var editing = await _repository.Get(id.Value);
        //
        //     if (editing == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (!ModelState.IsValid)
        //     {
        //         return View(nameof(Edit));
        //     }
        //
        //     // editing.Name = transaction.Name;
        //
        //     await _repository.Save(editing);
        //     TempData["message"] = "Account Saved";
        //     return RedirectToAction(nameof(Index));
        // }
        //
        // public async Task<IActionResult> Delete(long? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     await _repository.Delete(id.Value);
        //     TempData["message"] = "Account Deleted";
        //     return RedirectToAction(nameof(Index));
        // }
    }
}