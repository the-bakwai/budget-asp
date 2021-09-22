using System;
using System.ComponentModel.DataAnnotations;

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
    
    public interface ITransactionRepository: IRepository<Transaction>
    {
    }

    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BudgetContext context)
        {
            Context = context;
        }
    }
}