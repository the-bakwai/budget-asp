using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetAsp.Models
{
    public class Account : IEntity
    {
        public long Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
    }
    
    public interface IAccountRepository: IRepository<Account>
    {
        
    }

    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(BudgetContext context)
        {
            Context = context;
        }
    }
}