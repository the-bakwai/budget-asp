using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BudgetAsp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BudgetAsp.Models
{
    public class Transaction : IEntity
    {
        public long Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public string Memo { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; } = 0;

        public bool Cleared { get; set; }
        public bool Reconciled { get; set; }

        public long AccountId { get; set; }
        public Account Account { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public long PayeeId { get; set; }
        public Payee Payee { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }

    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetAllActiveByForAccount(long accountId);
        Task<TransactionsIndexViewModel> GetTransactionViewModel(long accountId);
    }

    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BudgetContext context)
        {
            Context = context;
        }

        public async Task<List<Transaction>> GetAllActiveByForAccount(long accountId)
        {
            return await BudgetContext.Transactions.Where(t => t.AccountId == accountId).OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<TransactionsIndexViewModel> GetTransactionViewModel(long accountId)
        {
            var account = await BudgetContext.Accounts.FindAsync(accountId);
            var transactions = await BudgetContext.Transactions.Where(t => t.AccountId == accountId)
                .Include(t => t.Account)
                .Include(t => t.Category)
                .Include(t => t.Payee)
                .Select(t => new TransactionViewModel(t.Id, t.Date, t.Memo, t.Category.Name, t.Payee.Name, t.Amount,
                    t.Cleared ? "Yes" : "No", t.Reconciled ? "Yes" : "No")).ToListAsync();

            return new TransactionsIndexViewModel(account, transactions);
        }

        public BudgetContext BudgetContext => Context as BudgetContext;
    }
}