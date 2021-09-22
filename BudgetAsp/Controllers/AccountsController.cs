using System.Threading.Tasks;
using BudgetAsp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAsp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _repository;

        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var accounts = await _repository.GetAllActiveOnly();
            
            return View(accounts);
        }

        public IActionResult New()
        {
            var account = new Account();
            
            return View(account);
        }

        public async Task<IActionResult> Create(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(New));
            }
            else
            {
                account.UserId = 1;
                
                await _repository.Save(account);
                TempData["message"] = "Account Saved";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var account = await _repository.Get(id.Value);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        public async Task<IActionResult> Update(long? id, Account account)
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
                return View(nameof(Edit));
            }

            editing.Name = account.Name;

            await _repository.Save(editing);
            TempData["message"] = "Account Saved";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _repository.Delete(id.Value);
            TempData["message"] = "Account Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}